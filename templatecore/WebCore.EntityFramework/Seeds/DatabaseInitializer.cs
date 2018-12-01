
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCore.Entities;
using WebCore.EntityFramework.Data;
using WebCore.Utils.Config;

namespace WebCore.EntityFramework.Seeds
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly WebCoreDbContext context;
        private readonly ILogger logger;
        private readonly UserManager<WebCoreUser> userManager;
        private readonly RoleManager<WebCoreRole> roleManager;

        private readonly List<BaseSeeder> seeders;

        public DatabaseInitializer(WebCoreDbContext context,
            UserManager<WebCoreUser> userManager,
            RoleManager<WebCoreRole> roleManager,
            ILogger<DatabaseInitializer> logger)
        {
            this.context = context;
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
            seeders = new List<BaseSeeder>()
            {
                new LanguageSeeder(),
                new LanguageDetailSeeder(),
                new SystemConfigSeeder()
            };
        }

        public async Task SeedAsync()
        {
            await context.Database.MigrateAsync().ConfigureAwait(false);

            if (!await context.Users.AnyAsync())
            {
                logger.LogInformation("Generating inbuilt accounts");

                const string adminRoleName = "admin";
                const string userRoleName = "user";

                await EnsureRoleAsync(adminRoleName, "Default administrator", new string[]
                {
                    ConstantConfig.Claims.Admin
                });
                await EnsureRoleAsync(userRoleName, "Default user", new string[] { });


                await CreateUserAsync("admin@webcore.com", "tempP@ss123", "Admin", "Last", "admin@webcore.com", "+1 (123) 000-0000", "Giáo viên", new string[] { adminRoleName });
                await CreateUserAsync("user@webcore.com", "tempP@ss123", "User", "Last", "user@webcore.com", "+1 (123) 000-0001", "Học sinh", new string[] { userRoleName });

                logger.LogInformation("Inbuilt account generation completed");
            }

            foreach (BaseSeeder seeder in seeders)
            {
                seeder.InitDb(context);
            }
        }



        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {


            if (await roleManager.RoleExistsAsync(roleName) == false)
            {
                WebCoreRole webCoreRole = new WebCoreRole(roleName, description);


                IdentityResult result = await roleManager.CreateAsync(webCoreRole);
                foreach (string claim in claims)
                {
                    await roleManager.AddClaimAsync(webCoreRole, new Claim(ConstantConfig.ClaimType.Permission
                        , claim));
                }
                if (!result.Succeeded)
                {
                    throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Errors.ToArray().Select(x => x.Description))}");
                }

            }
        }

        private async Task<WebCoreUser> CreateUserAsync(string userName
            , string password
            , string firstName
            , string lastName
            , string email
            , string phoneNumber
            , string carrer
            , string[] roles)
        {
            WebCoreUser webCoreUser = new WebCoreUser
            {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Carrer = carrer,
                EmailConfirmed = true,
                RecordStatus = 0
            };

            IdentityResult result = await userManager.CreateAsync(webCoreUser, password);

            foreach (string role in roles)
            {
                await userManager.AddToRoleAsync(webCoreUser, role);
            }

            if (!result.Succeeded)
            {
                throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Errors.ToArray().Select(x => x.Description))}");
            }

            return webCoreUser;
        }
    }
}
