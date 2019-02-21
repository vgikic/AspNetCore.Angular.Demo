using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Portal.DAL;
using Portal.Hubs;
using Portal.Identity;
using Portal.Models.Configuration;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Angular.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AppDbCtx>(options => options.UseSqlServer(Configuration.GetConnectionString("DemoDb")));
            services.AddCors();
            services.AddDataProtection();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSignalR();

            // For demonstration simplicity, only minimum requirements are set
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 4;
                options.User.RequireUniqueEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.SignIn.RequireConfirmedEmail = false;
            })
              .AddUserStore<UserStore>()
              .AddRoleStore<RoleStore>()
              .AddSignInManager<SignInManager<User>>()
              .AddDefaultTokenProviders();


            var jwtSettings = GetJwtSettings();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),

                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(jwtSettings.MinutesToExpiration)
                };

            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            //services.AddScoped<IHostingEnvironment>((sp) => sp.GetServices( .CreateScope() this.Environment);


            services.AddAuthorization(options =>
            {
                options.AddPolicy(RoleClaims.Admin, policy => policy.RequireClaim(RoleClaims.Admin));
                options.AddPolicy(RoleClaims.Support, policy => policy.RequireClaim(RoleClaims.Support));
                options.AddPolicy(RoleClaims.God, policy => policy.RequireClaim(RoleClaims.God));

            });

            services.AddSingleton<IHostingEnvironment>(Environment);
            services.AddScoped<AuthenticationService>();
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            services.AddAutoMapper(c => c.CreateMissingTypeMaps = true);

            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RoleManager<Role> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();


            if (env.IsProduction())
            {
                //  serve log files
                var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
                var ctProvider = new FileExtensionContentTypeProvider();
                ctProvider.Mappings[".log"] = "text/html";
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(logDirectory),
                    RequestPath = "/logs",
                    ContentTypeProvider = ctProvider
                });
            }


#if DEBUG
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowCredentials()
            );
#endif



            app.UseSignalR(routes => routes.MapHub<ItemHub>("/itemhub")); // Url is used by client

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });

            SeedRoles(roleManager).Wait();
        }

        private JwtSettings GetJwtSettings()
        {
            return new JwtSettings
            {
                Audience = Configuration["JwtSettings:audience"],
                Issuer = Configuration["JwtSettings:issuer"],
                Key = Configuration["JwtSettings:key"],
                MinutesToExpiration = Convert.ToInt32(Configuration["JwtSettings:minutesToExpiration"])
            };
        }
        private async Task SeedRoles(RoleManager<Role> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(RoleClaims.Admin))
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "Admin",
                    NormalizedName = RoleClaims.Admin,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                });
            }
            if (!await roleManager.RoleExistsAsync(RoleClaims.Support))
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "Support",
                    NormalizedName = RoleClaims.Support,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                });
            }
            if (!await roleManager.RoleExistsAsync(RoleClaims.God))
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "God",
                    NormalizedName = RoleClaims.God,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                });
            }
        }
    }
}
