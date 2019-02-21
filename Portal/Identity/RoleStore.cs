using Microsoft.AspNetCore.Identity;
using Portal.DAL;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.Identity
{
    public class RoleStore : IRoleStore<Role>
    {
        private readonly AppDbCtx ctx;

        public RoleStore(AppDbCtx ctx, IdentityErrorDescriber describer)
        {
            this.ctx = ctx;
        }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await ctx.AddAsync(role);
            await ctx.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return ctx.FindAsync<Role>(int.Parse(roleId));
        }
        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(ctx.Roles.FirstOrDefault((System.Linq.Expressions.Expression<System.Func<Role, bool>>)(r => r.NormalizedName == normalizedRoleName)));
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.NormalizedName);
        }
        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (ctx.UserRoles.Any(ur=>ur.RoleId == role.Id))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"Role {role.NormalizedName} is attached to users"
                });
            }

            ctx.Remove(role);
            await ctx.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            role.Name = roleName;
            role.NormalizedName = roleName.ToUpperInvariant();
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await ctx.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.NormalizedName);
        }
        public void Dispose() { }

    }
}
