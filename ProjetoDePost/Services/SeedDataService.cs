/*using Microsoft.AspNetCore.Identity;
using ProjetoDePost.Models;

public static class SeedDataService
{
    public static async Task CriarUsuarioAdministrador(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Definir a role de administrador global
        var adminRole = "AdminGlobal";
        var adminEmail = "admin@admin.com"; // Defina o e-mail do administrador
        var adminPassword = "SenhaForte123!"; // Senha inicial para o administrador

        

        // Log de depuração para criação da role
        Console.WriteLine("Verificando a criação da role 'AdminGlobal'...");

        // Criar a role "AdminGlobal" se ela não existir
        if (await roleManager.FindByNameAsync(adminRole) == null)
        {
            Console.WriteLine("Role 'AdminGlobal' não encontrada. Criando...");
            var roleResult = await roleManager.CreateAsync(new IdentityRole(adminRole));
            if (!roleResult.Succeeded)
            {
                Console.WriteLine($"Erro ao criar role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                throw new Exception($"Erro ao criar a role '{adminRole}': {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }
            else
            {
                Console.WriteLine("Role 'AdminGlobal' criada com sucesso.");
            }
        }
        else
        {
            Console.WriteLine($"A role '{adminRole}' já existe.");
        }

        // Verificar se o usuário administrador já existe
        var user = await userManager.FindByEmailAsync(adminEmail);

        if (user == null)
        {
            // Se o usuário não existir, criá-lo
            Console.WriteLine($"Usuário '{adminEmail}' não encontrado. Criando o usuário administrador...");
            user = new Usuario
            {
                UserName = adminEmail,
                Email = adminEmail,
                // Adicione outras propriedades do usuário, como Nome, etc., se necessário
            };

            var result = await userManager.CreateAsync(user, adminPassword);

            if (result.Succeeded)
            {
                Console.WriteLine($"Usuário '{adminEmail}' criado com sucesso.");

                // Atribuir a role de AdminGlobal ao novo usuário
                Console.WriteLine($"Atribuindo a role '{adminRole}' ao usuário '{adminEmail}'...");
                var addRoleResult = await userManager.AddToRoleAsync(user, adminRole);
                if (!addRoleResult.Succeeded)
                {
                    Console.WriteLine($"Erro ao adicionar a role '{adminRole}' ao usuário: {string.Join(", ", addRoleResult.Errors.Select(e => e.Description))}");
                    throw new Exception($"Erro ao adicionar a role '{adminRole}' ao usuário: {string.Join(", ", addRoleResult.Errors.Select(e => e.Description))}");
                }
                else
                {
                    Console.WriteLine($"Role '{adminRole}' atribuída ao usuário '{adminEmail}' com sucesso.");
                }
            }
            else
            {
                Console.WriteLine($"Erro ao criar o usuário administrador: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                throw new Exception($"Erro ao criar o usuário administrador: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            // Se o usuário já existir, apenas adicione a role se ele não tiver
            Console.WriteLine($"Usuário '{adminEmail}' já existe. Verificando se a role '{adminRole}' já foi atribuída...");
            if (!await userManager.IsInRoleAsync(user, adminRole))
            {
                Console.WriteLine($"Usuário '{adminEmail}' não tem a role '{adminRole}'. Atribuindo...");
                var addRoleResult = await userManager.AddToRoleAsync(user, adminRole);
                if (!addRoleResult.Succeeded)
                {
                    Console.WriteLine($"Erro ao adicionar a role '{adminRole}' ao usuário: {string.Join(", ", addRoleResult.Errors.Select(e => e.Description))}");
                    throw new Exception($"Erro ao adicionar a role '{adminRole}' ao usuário: {string.Join(", ", addRoleResult.Errors.Select(e => e.Description))}");
                }
                else
                {
                    Console.WriteLine($"Role '{adminRole}' atribuída ao usuário '{adminEmail}' com sucesso.");
                }
            }
            else
            {
                Console.WriteLine($"Usuário '{adminEmail}' já possui a role '{adminRole}'.");
            }
        }
    }
}
*/