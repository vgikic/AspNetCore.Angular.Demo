using Angular.Core.Models.Dto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Portal.DAL;
using Portal.Models.Configuration;
using Portal.Models.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Identity
{
    public class AuthenticationService
    {
        private readonly AppDbCtx ctx;
        private readonly JwtSettings jwtSettings;



        public AuthenticationService(IOptions<JwtSettings> jwtSettings, AppDbCtx ctx)
        {
            this.jwtSettings = jwtSettings.Value;
            this.ctx = ctx;
        }


        protected string BuildJwtToken(UserAuthDto authUser)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));

            var jwtClaims = new List<Claim>
            {
                // Standard JWT claims
                new Claim(JwtRegisteredClaimNames.Sub, authUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                // Custom claims
                //new Claim(ADMIN,authUser.UserName),
            };

            foreach (var claim in authUser.Claims)
            {
                jwtClaims.Add(new Claim(claim.ClaimType, claim.ClaimType));
            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: jwtClaims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.MinutesToExpiration),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        private static List<ClaimDto> claims = new List<ClaimDto>
        {
            new ClaimDto
            {
               Id = 1,
               RoleId = 1,
               ClaimType = RoleClaims.Admin,

            },
               new ClaimDto
            {
               Id = 2,
               RoleId = 2,
               ClaimType = RoleClaims.Support,
            },
               new ClaimDto
            {
               Id = 3,
               RoleId = 3,
               ClaimType = RoleClaims.God,
            },
        };

        //public UserDto ValidateUser(LoginBindingModel user)
        //{

        //}

        public UserAuthDto BuildUserAuthObject(User user)
        {
            var authUser = new UserAuthDto
            {
                Claims = claims.Where(c => user.UserRoles.Any(ur => ur.RoleId == c.RoleId)),
                UserName = user.UserName,
                IsAuthenticated = true,
                Id = user.Id,
            };
            authUser.BearerToken = BuildJwtToken(authUser);
            return authUser;

        }

        public async Task UpdateLoginTimeAsync(User user)
        {
            user.LastLogin = DateTime.UtcNow;
            await ctx.SaveChangesAsync();
        }

    }
}
