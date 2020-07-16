using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace WebAppMvc.Data
{
    public class SeedData
    {
        public const string DefaultEmail = "admin@ibestread.com";
        public const string DefaultPassword = "1q2w3E*";

        public static async Task InitializeDefaultUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            await EnsureUser(userManager, DefaultEmail, DefaultPassword);
        }

        private static async Task EnsureUser(
            UserManager<IdentityUser> userManager,
            string userName, 
            string userPassword)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new IdentityUser(userName)
                {
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, userPassword);
            }
        }
    }
}