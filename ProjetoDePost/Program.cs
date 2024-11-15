using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoDePost.Data;
using ProjetoDePost.Data.Repositories.Implementations;
using ProjetoDePost.Data.Repositories.Implementations.Generic;
using ProjetoDePost.Data.Repositories.Interfaces;
using ProjetoDePost.Data.Repositories.Interfaces.Generic;
using ProjetoDePost.Data.Validations;
using ProjetoDePost.Models;
using ProjetoDePost.Services.Implementations;
using ProjetoDePost.Services.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using ProjetoDePost.Services;
using ProjetoDePost.Authorization;
using Microsoft.AspNetCore.Authorization;
using ProjetoDePost.Profiles;
using Serilog;
using ProjetoDePost.Middleware;
using System.Security.Claims;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Adiciona a configuração de segurança
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insira o token JWT com o prefixo 'Bearer '",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    
    

    // Define o uso do Bearer no Swagger
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
    
builder.Services.AddSwaggerGen();
// Registrar o ProjetoDePostContext com MySQL
builder.Services.AddDbContext<ProjetoDePostContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("ProjetoDePostConnection"),
        new MySqlServerVersion(new Version(8, 0, 23))
    ));

builder.Services.AddAuthorization(options =>
{
    // Política para AdminGlobal: Acesso a tudo na aplicação
    options.AddPolicy("AdminGlobal", policy =>
        policy.RequireRole("AdminGlobal"));

   
    /*
    // Política para AdminEmpresarial: Acesso a empresas específicas
    options.AddPolicy("AdminEmpresarial", policy =>
        policy.Requirements.Add(new EmpresaAdminRequirement()));
    */
});

// Configura o Identity para usar o modelo de usuário personalizado
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<ProjetoDePostContext>()
    .AddDefaultTokenProviders();


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
        RoleClaimType = ClaimTypes.Role,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    })
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<SolicitacaoCadastroEmpresaDtoValidator>();
        config.RegisterValidatorsFromAssemblyContaining<EmpresaCreateDtoValidator>();
        config.RegisterValidatorsFromAssemblyContaining<UsuarioCreateDtoValidator>();

    });
// Configuração do AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
void RegisterServices(IServiceCollection services)
{
    services.AddScoped<IUsuarioService, UsuarioService>();
    services.AddScoped<IEmpresaService, EmpresaService>();
    services.AddScoped<ISolicitacaoCadastroEmpresaService, SolicitacaoCadastroEmpresaService>();
    services.AddScoped<IParticipanteEmpresaService, ParticipanteEmpresaService>();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IAuthorizationHandler, EmpresaAdminAuthorizationHandler>();
    services.AddScoped<ICampanhaService, CampanhaService>();
    services.AddScoped<IPostagemService, PostagemService>();
    services.AddScoped<IOpenAiService, OpenAiService>();
    services.AddScoped<IHistoricoCampanhaService,  HistoricoCampanhaService>();
    services.AddScoped<INotificacaoService, NotificacaoService>();
    services.AddScoped<IAuthService,AuthService>();
    services.AddScoped<AuthService>();
    services.AddSingleton<TokenRevocationService>();
}
void RegisterRepositories(IServiceCollection services)
{
    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    services.AddScoped<IEmpresaRepository, EmpresaRepository>();
    services.AddScoped<ISolicitacaoCadastroEmpresaRepository, SolicitacaoCadastroEmpresaRepository>();
    services.AddScoped<IParticipanteEmpresaRepository, ParticipanteEmpresaRepository>();
    services.AddScoped<ICampanhaRepository, CampanhaRepository>();
    services.AddScoped<IPostagemRepository,  PostagemRepository>();
    services.AddScoped<ISolicitacaoCampanhaRepository, SolicitacaoCampanhaRepository>();
    services.AddScoped<IHistoricoCampanhaRepository, HistoricoCampanhaRepository>();
}
// Configuração do log

builder.Logging.ClearProviders(); 
builder.Logging.AddConsole();
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
app.UseMiddleware<TokenRevocationMiddleware>();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var tokenRevocationService = services.GetRequiredService<TokenRevocationService>();
        // Chama o método de inicialização para criar roles e o AdminGlobal
        await SeedDataService.InitializeAsync(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao inicializar o seed: {ex.Message}");
    }
}
app.Run();
