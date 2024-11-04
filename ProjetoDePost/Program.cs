using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data;
using ProjetoDePost.Data.Repositories.Implementations;
using ProjetoDePost.Data.Repositories.Implementations.Generic;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Data.Repositories.Interfaces.Generic;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Implementations;
using ProjetoDePost.Services.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Registrar o ProjetoDePostContext com MySQL
builder.Services.AddDbContext<ProjetoDePostContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("ProjetoDePostConnection"),
        new MySqlServerVersion(new Version(8, 0, 23))
    ));

// Configura o Identity para usar o modelo de usuário personalizado
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<ProjetoDePostContext>()
    .AddDefaultTokenProviders();
//builder.Services.AddHttpClient<OpenAiService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

void RegisterServices(IServiceCollection services)
{
    //services.AddScoped<IOpenAiService, OpenAiService>();
    services.AddScoped<IUsuarioService, UsuarioService>();

}

void RegisterRepositories(IServiceCollection services)
{
    services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
    services.AddScoped<IUsuarioRepository, UsuarioRepository>();
}

// Funções para registar os serviços e repositórios.
RegisterServices(builder.Services);
RegisterRepositories(builder.Services);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
