using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVTQ.LogifySearch.Application.Interfaces.Repositories;
using MVTQ.LogifySearch.Application.Interfaces.Services;
using MVTQ.LogifySearch.Domain.Entities;
using MVTQ.LogifySearch.Infrastructure.Database;
using MVTQ.LogifySearch.Infrastructure.Database.Repositories;
using MVTQ.LogifySearch.Infrastructure.Identity;
using MVTQ.LogifySearch.Infrastructure.Services;
namespace MVTQ.LogifySearch.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //Register Service
            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            services.AddScoped(typeof(IUserService), typeof(UserService));

            //Register Persistence
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddDbContext<LogifySearchDbContext>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"])
            );
            //Register Identity
            services.AddIdentity<User, IdentityRole>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false; 
                options.Password.RequireLowercase = false; 
                options.Password.RequireNonAlphanumeric = false; 
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3; 
                options.Password.RequiredUniqueChars = 0;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5; 
                options.Lockout.AllowedForNewUsers = true;
                
                options.User.AllowedUserNameCharacters =
                    "abcdeghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                options.User.RequireUniqueEmail = true;  

                options.SignIn.RequireConfirmedEmail = false;           
                options.SignIn.RequireConfirmedPhoneNumber = false;     
            })
            .AddEntityFrameworkStores<LogifySearchDbContext>()
            .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });
            //services.AddTransient<IClaimsTransformation, ClaimTranformer>();


            return services;
        }
       
    }
}
