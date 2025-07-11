using AduanasTec.Database;
using AduanasTec.Repositories;
using AduanasTec.Repositories.Interfaces;
using AduanasTec.Services;
using AduanasTec.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AduanasTec.Swagger;

var builder = WebApplication.CreateBuilder(args);

// 1) Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 2) Configurar Swagger/OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    // Documento "v1"
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AduanasTec",
        Version = "v1",
        Description = "API REST de AduanasTec (Productos, Clientes, Ventas)"
    });

    // JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Usa: Bearer {tu_token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            new List<string>()
        }
    });

    // Aplica candado a los endpoints con [Authorize]
    c.OperationFilter<AuthorizationOperationFilter>();
});

// 3) Configurar EF Core + SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 4) Inyección de dependencias: Services
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// 5) Inyección de dependencias: Repositories
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();

// 6) Autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// 7) Middleware pipeline

// **Siempre** exponer Swagger y su UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Swagger JSON
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AduanasTec API v1");
    // Si quieres servir la UI en la raíz en lugar de /swagger:
    // c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// SEEDER: Crear usuario admin si no existe
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AduanasTec.Database.AppDbContext>();
    AduanasTec.Database.DbSeeder.Seed(db);
}

app.Run();
