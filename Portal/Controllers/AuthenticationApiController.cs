using Angular.Core.Controllers;
using Angular.Core.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Portal.DAL;
using Portal.Identity;
using Portal.Models.BindingModel;
using Portal.Models.Configuration;
using System;
using System.Security;
using System.Threading.Tasks;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationApiController : BaseApiController
    {
        private JwtSettings jwtSettings;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly AuthenticationService authService;
        private readonly SignInManager<User> signInManager;
        private readonly AppDbCtx ctx;

        public AuthenticationApiController(
                                AppDbCtx ctx,
                                IMapper mapper,
                                RoleManager<Role> roleManage,
                                UserManager<User> userManager,
                                RoleManager<Role> roleManager,
                                IOptions<JwtSettings> jwtSettings,
                                SignInManager<User> signInManager,
                                AuthenticationService authService,
                                ILogger<AuthenticationApiController> logger) : base(logger, mapper)
        {
            this.ctx = ctx;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.authService = authService;
            this.signInManager = signInManager;
            this.jwtSettings = jwtSettings.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginBindingModel model)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                var response = new UserAuthDto();

                if (user == null)
                {
                    response.InvalidCredentials = true;
                    return Ok(response);
                }
                if (!user.IsActive)
                {
                    response.IsInactive = true;
                    return Ok(response);
                }

                var signInResult = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: true);
                logger.LogInformation($"User: '{model}' signed in result: {signInResult}");
                if (signInResult.IsLockedOut)
                {
                    response.IsLocked = true;
                    return Ok(response);
                }
                if (signInResult.Succeeded)
                {
                    await authService.UpdateLoginTimeAsync(user);
                    response = this.authService.BuildUserAuthObject(user);
                    return Ok(response);
                }
                else
                {
                    response.InvalidCredentials = true;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] LoginBindingModel model)
        {
            using (var transaction = await ctx.Database.BeginTransactionAsync())
            {
                try
                {
                    if (model == null || !ModelState.IsValid)
                        return BadRequest(ModelState);

                    var user = new User()
                    {
                        Email = model.Email,
                        UserName = model.Email,
                        NormalizedEmail = model.Email.ToUpperInvariant()
                    };

                    var identityResult = await userManager.CreateAsync(user);

                    if (identityResult.Succeeded)
                    {
                        for (int id = 1; id < 3; id++)
                        {
                            var ur = new UserRole()
                            {
                                RoleId = id,
                                UserId = user.Id,
                                Role = ctx.Roles.Find(id)
                            };
                            ctx.UserRoles.Add(ur);
                        }

                        user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.Password);
                        user.IsActive = true;

                        await ctx.SaveChangesAsync();
                        transaction.Commit();
                        return NoContent();
                    }

                    foreach (var error in identityResult.Errors)
                        ModelState.AddModelError(error.Code, error.Description);

                    return BadRequest(ModelState);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    LogError(ex);
                    return BadRequest(ex.Message);
                    throw;
                }
            }
        }



        [Authorize]
        [HttpGet("signout")]
        public async Task<IActionResult> LogOutAsync()
        {
            try
            {
                await signInManager.SignOutAsync();
                logger.LogInformation($"{User.Identity.Name} signed out.");
                return NoContent();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }



    }
}