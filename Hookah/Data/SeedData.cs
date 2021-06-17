using Hookah.Abstracts;
using Hookah.Constants;
using Hookah.Models;
using Hookah.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // Look for any TODO items.
                /*  if (dbContext.TestEntities.Any())
                  {
                      return;   // DB has been seeded
                  }
  */           
                await AddRoles(serviceProvider);
                await AddUsers(serviceProvider);
                var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                await unitOfWork.CommitAsync();
            }
        }

        private static async Task AddRoles(IServiceProvider serviceProvider)
        {
            try
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                if (!await roleManager.RoleExistsAsync(RoleNames.Admin))
                    await roleManager.CreateAsync(new IdentityRole<Guid>() { Name = RoleNames.Admin });
                if (!await roleManager.RoleExistsAsync(RoleNames.SuperAdmin))
                    await roleManager.CreateAsync(new IdentityRole<Guid>() { Name = RoleNames.SuperAdmin });
            }
            catch (Exception e)
            {
                throw new Exception(ExceptionMessages.RolesNotCreated);
            }
        }

        private static async Task AddUsers(IServiceProvider serviceProvider)
        {
            try
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var userName = config.GetValue<string>("SuperAdminDetails:UserName");
                var superAdminUser = await userManager.FindByNameAsync(userName);
                if (superAdminUser == null)
                {
                    superAdminUser = new User()
                    {
                        FirstName = config.GetValue<string>("SuperAdminDetails:FirstName"),
                        LastName = config.GetValue<string>("SuperAdminDetails:LastName"),
                        Email = config.GetValue<string>("SuperAdminDetails:Email"),
                        PhoneNumber = config.GetValue<string>("SuperAdminDetails:PhoneNumber"),
                        UserName = config.GetValue<string>("SuperAdminDetails:UserName"),
                        IsSuperAdmin = true,
                    };
                    var result = await userManager.CreateAsync(superAdminUser, config.GetValue<string>("SuperAdminDetails:Password"));
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(superAdminUser, RoleNames.SuperAdmin);
                }
            }
            catch (Exception e)
            {
                throw new Exception(ExceptionMessages.UsersNotCreated);
            }
        }
    }
}
