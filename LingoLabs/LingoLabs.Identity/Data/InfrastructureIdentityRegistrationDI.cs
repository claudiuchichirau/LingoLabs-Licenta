using LingoLabs.Application.Contracts.Identity;
using LingoLabs.Identity.Models;
using LingoLabs.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LingoLabs.Identity.Data
{
    public static class InfrastructureIdentityRegistrationDI
    {
        public static IServiceCollection AddInfrastructureIdentityToDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LingoLabsIdentityDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("LingoLabsUserConnection"),
                    builder =>
                    builder.MigrationsAssembly(typeof(LingoLabsIdentityDbContext).Assembly.FullName)
                    ));

            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<LingoLabsIdentityDbContext>()
                            .AddDefaultTokenProviders();

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                        // Adding Jwt Bearer  
                        .AddJwtBearer(options =>
                        {
                            options.SaveToken = true;
                            options.RequireHttpsMetadata = false;
                            options.TokenValidationParameters = new TokenValidationParameters()
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidAudience = configuration["JWT:ValidAudience"],
                                ValidIssuer = configuration["JWT:ValidIssuer"],
                                ClockSkew = TimeSpan.Zero,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                            };
                        });
            services.AddScoped<
                ILoginService, LoginService>();
            services.AddScoped<
                IRegistrationServiceStrategy, AdminRegistrationServiceStrategy>();
            services.AddScoped<
                IRegistrationServiceStrategy, StudentRegistrationServiceStrategy>();
            services.AddScoped<GetRegistrationStrategy>();
            services.AddScoped<
                IRegistrationServiceStrategy, InvalidRoleRegistrationServiceStrategy>();
            services.AddScoped<
                IUserService, UserService>();

            return services;
        }
    }
}
