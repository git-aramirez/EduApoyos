using Microsoft.AspNetCore.Identity;

namespace EduApoyos.API.Data
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Asesor"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Asesor"));
            }

            if (!await roleManager.RoleExistsAsync("Estudiante"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Estudiante"));
            }
        }
    }
}
