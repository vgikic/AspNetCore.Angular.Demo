namespace Portal.Identity
{
    #region usings
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Portal.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    #endregion

    public class UserStore : IUserRoleStore<User>, IUserPasswordStore<User>, IUserLockoutStore<User>, IUserEmailStore<User>
    {

        private AppDbCtx ctx { get; }

        public UserStore(AppDbCtx ctx)
        {
            this.ctx = ctx;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            await ctx.AddAsync(user);
            await ctx.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await ctx.Users.FirstOrDefaultAsync(u => u.UserName == normalizedUserName);
            return user;
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.UserName);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await ctx.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.CompletedTask;
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var role = await ctx.Roles.FirstOrDefaultAsync(r => r.NormalizedName == roleName);
            if (role == null)
                throw new ArgumentNullException("role", $"{roleName} does not exist in DB");
            await ctx.UserRoles.AddAsync(new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id
            });
            await ctx.SaveChangesAsync();
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var roles = await ctx.Roles
                                .Include(r => r.UserRoles)
                                .Where(r => r.UserRoles.Any(ur => ur.UserId == user.Id))
                                .Select(r => r.NormalizedName)
                                .ToListAsync();
            return roles;
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await ctx.UserRoles
                              .Include(r => r.Role)
                              .AnyAsync(r => r.UserId == user.Id && r.Role.NormalizedName == roleName);
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var userRoleToDelete = await ctx.UserRoles.Include(ur => ur.Role)
                                              .FirstOrDefaultAsync(r => r.UserId == user.Id && r.Role.NormalizedName == roleName);

            ctx.Remove(userRoleToDelete);
            await ctx.SaveChangesAsync();
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        Task<string> IUserStore<User>.GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.Id.ToString());
        }

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.LockoutEnd);
        }

        public async Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.LockoutEnd = lockoutEnd;
            await ctx.SaveChangesAsync();
        }

        public async Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ++user.AccessFailedCount;
            await ctx.SaveChangesAsync();
            return user.AccessFailedCount;
        }

        public async Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.AccessFailedCount = 0;
            await ctx.SaveChangesAsync();
        }

        public Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.LockoutEnabled.Value);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.LockoutEnabled = enabled;
            return Task.CompletedTask;
        }

        public async Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.Email = email;
            await ctx.SaveChangesAsync();
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.EmailConfirmed);
        }

        public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.EmailConfirmed = confirmed;
            await ctx.SaveChangesAsync();
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await ctx.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);
            return user;
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.NormalizedEmail = user.Email.ToUpperInvariant();
            user.UserName = user.Email;
            return Task.CompletedTask;
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (int.TryParse(userId, out int id))
            {
                var user = await ctx.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.Id == id);
                return user;
            }
            throw new ArgumentException("userId not int");
        }

        #region Unused methods
        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
