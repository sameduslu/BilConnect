
using System.Collections.Generic;
using System.Linq;
using System;
using BilConnect.Models;
using Microsoft.AspNetCore.Identity;
using BilConnect.Data.Static;

namespace Bilconnect_First_Version.data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            
           
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.ClubAccount))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.ClubAccount));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@bilconnect.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Ataturk1881@");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }



                string appUserEmail1 = "user1@ug.bilkent.edu.tr";
                var appUser1 = await userManager.FindByEmailAsync(appUserEmail1);
                if (appUser1 == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "User 1",
                        UserName = "user1@ug.bilkent.edu.tr",
                        Email = appUserEmail1,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "TaylorSwift13?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string appUserEmail2 = "user2@ug.bilkent.edu.tr";
                var appUser2 = await userManager.FindByEmailAsync(appUserEmail2);
                if (appUser2 == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "User 2",
                        UserName = "user2@ug.bilkent.edu.tr",
                        Email = appUserEmail2,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "TaylorSwift13?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string appUserEmail3 = "user3@ug.bilkent.edu.tr";
                var appUser3 = await userManager.FindByEmailAsync(appUserEmail3);
                if (appUser3 == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "User 3",
                        UserName = "user3@ug.bilkent.edu.tr",
                        Email = appUserEmail3,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "TaylorSwift13?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string appUserEmail4 = "bilkentacm@ug.bilkent.edu.tr";
                var appUser4 = await userManager.FindByEmailAsync(appUserEmail4);
                if (appUser4 == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Bilkent ACM",
                        UserName = "bilkentacm@ug.bilkent.edu.tr",
                        Email = appUserEmail4,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Taytay*13");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.ClubAccount);
                }
            }
        }
    }
}
