using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressTaxi.Data;

using ExpressTaxi.Domain;

namespace ExpressTaxi.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;


            await RoleSeeder(services);
            await SeedAdministrator(services);

            var dataBrand = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedBrands(dataBrand);

            var dataOption = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedOptions(dataOption);


            return app;
        }
        private static void SeedBrands(ApplicationDbContext dataBrand)
        {
            if (dataBrand.Brands.Any())
            {
                return;
            }
            dataBrand.Brands.AddRange(new[]
            {
                new Brand {Name="Kia"},
                new Brand {Name="Mercedes"},
                new Brand {Name="Renault"},
                new Brand {Name="Dacia"}
            });
            dataBrand.SaveChanges();
        }
        private static void SeedOptions(ApplicationDbContext dataOption)
        {
            if (dataOption.Brands.Any())
            {
                return;
            }
            dataOption.Options.AddRange(new[]
            {
                new Option {Name="Англоговорящ шофьор"},
                new Option {Name="Плащане с карта"},
                new Option {Name="Много багаж"}
            });
            dataOption.SaveChanges();
        }



        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Administrator", "Client" };

            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }


        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<TaxiUser>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                TaxiUser user = new TaxiUser();
                user.FirstName = "admin";
                user.LastName = "admin";
                user.PhoneNumber = "123456789";
                user.UserName = "admin";
                user.Email = "admin@admin.com";

                var result = await userManager.CreateAsync
                (user, "123456");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

    }
}