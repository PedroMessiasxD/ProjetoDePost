using Microsoft.AspNetCore.Identity;
using ProjetoDePost.Models;

public static class SeedDataService
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();

        // Definindo as roles que queremos no sistema
        string[] roleNames = { "AdminGlobal", "AdminEmpresarial", "ParticipanteEmpresa", "Usuario" };

        foreach (var roleName in roleNames)
        { 
        
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var adminUser = new Usuario
        {
            UserName = "adminGlobal",
            Email = "admin@projetodepost.com",
            EmailConfirmed = true
        };

        var user = await userManager.FindByEmailAsync(adminUser.Email);
        if (user == null)
        {
            // Cria o usuário com a senha padrão
            var createPowerUser = await userManager.CreateAsync(adminUser, "Admin@12345");
            if (createPowerUser.Succeeded)
            {
                // Adiciona a role AdminGlobal ao usuário criado
                await userManager.AddToRoleAsync(adminUser, "AdminGlobal");
            }
        }
    } 
}
