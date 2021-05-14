using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj_AGL.Data
{
    public class Seed
    {
        public static void SeedRoles(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                string[] roles = new string[] { "Admin" };

                foreach (string role in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(context);

                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        roleStore.CreateAsync(new IdentityRole(role));
                        context.SaveChangesAsync();
                    }
                }
            }


            string username = "admin@ua.edu.lb";
            var userStore = new UserStore<IdentityUser>(context);

            if (!context.Users.Any(r => r.Email == username))
            {
                userStore.CreateAsync(new IdentityUser(username));
                context.SaveChangesAsync();
            }

            var userId = context.Users.FirstOrDefault(r => r.UserName == username).Id;
            var roleId = context.Roles.FirstOrDefault(r => r.Name == "Admin").Id;

            context.UserRoles.Add(new IdentityUserRole<string> { UserId = userId, RoleId = roleId });
            context.SaveChangesAsync();
        }
    }
}
