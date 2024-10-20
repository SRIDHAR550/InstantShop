using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantShop.Infrastructure.Seed
{
    public class DataSeed
    {
        public static async Task RoleSeed(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new List<IdentityRole> {
                new  IdentityRole{Name="ADMIN",NormalizedName="ADMIN"},
                new  IdentityRole{Name="CUSTOMER",NormalizedName="CUSTOMER"}
            };
            foreach (var role in roles)
            {
                if (!await rolemanager.RoleExistsAsync(role.Name))
                {
                    await rolemanager.CreateAsync(role);
                }
            }
        }
    }
}
