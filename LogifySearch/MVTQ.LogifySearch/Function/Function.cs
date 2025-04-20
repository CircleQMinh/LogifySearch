using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVTQ.LogifySearch.Domain.Entities;
using MVTQ.LogifySearch.Domain.Enums;
using MVTQ.LogifySearch.Domain.Extensions;
using MVTQ.LogifySearch.Domain.Setting;
using MVTQ.LogifySearch.Infrastructure.Database;

namespace MVTQ.LogifySearch.Function
{
    public static class Function
    {
        public static async void EnsureDataBaseCreated(this WebApplication? app, IConfiguration configuration)
        {
            if (app != null)
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<LogifySearchDbContext>();
                    db.Database.EnsureCreated();
                    await SeedRolesAndUsers(scope.ServiceProvider, configuration);
                }
            }
        }

        public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var options = serviceProvider.GetRequiredService<IOptions<DefaultUserSetting>>();

            string[] roleNames = {
                AppRole.Admin.GetDescription(), 
                AppRole.Manager.GetDescription(),
                AppRole.User.GetDescription() 
            };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create users if needed

            var admins = options.Value.Admins;
            var managers = options.Value.Managers;
            var users = options.Value.Users;

            await CreateUsersInRoleAsync(admins, AppRole.Admin.GetDescription(), userManager);
            await CreateUsersInRoleAsync(managers, AppRole.Manager.GetDescription(), userManager);
            await CreateUsersInRoleAsync(users, AppRole.User.GetDescription(), userManager);


        }

        public static async Task CreateUsersInRoleAsync(IEnumerable<CreateUserModelSetting> users, string role, UserManager<User> userManager)
        {
            foreach (var item in users)
            {
                var existingUser = await userManager.FindByEmailAsync(item.UserName);
                if (existingUser != null) continue;

                var user = new User
                {
                    UserName = item.UserName,
                    Email = item.Email,
                    CreatedBy = item.UserName,
                    UpdatedBy = item.UserName,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                var result = await userManager.CreateAsync(user, item.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
