using System.Text;
using Application.Common;
using Application.Interfaces;
using Application.UseCase.Usuarios;
using Application.UseCases;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Custom
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<UsuarioContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioQuery, UsuarioQuery>();
builder.Services.AddScoped<IUsuarioCommand, UsuarioCommand>();


//builder.Services.AddScoped<IRecetaService, RecetaService>();

//CORS deshabilitar
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//JWT CONFIGURATION
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);


//firma
var appSettings = appSettingsSection.Get<AppSettings>();
var firma = appSettings.Secret;

builder.Services.AddScoped<ITokenService, TokenService>(ServiceProvider =>
{
    return new TokenService(firma);
});

//agregado servicio de token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
{
    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(appSettings.Secret)
        ),
        ValidIssuer = "localhost",
        ValidAudience = "usuarios",
        ValidateLifetime = true
    };
});


builder.Services.AddHttpClient(); // Configuración de HttpClient


// Add services to the container.
builder.Services.AddDataProtection();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservicio Usuarios", Version = "v1" });

    // Configuración para JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Introduce el token JWT con 'Bearer ' al inicio",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Endpoint para establecer la preferencia de tema del usuario
//app.MapPost("/set-theme", (HttpContext context, string theme) =>
//{
//    if (theme == "dark" || theme == "light") // Validar entrada
//    {
//        context.Session.SetString("userTheme", theme);
//        return Results.Ok(new { Message = $"Tema {theme} establecido correctamente." });
//    }
//    return Results.BadRequest(new { Message = "Tema no válido." });
//});

// Endpoint para obtener la preferencia de tema del usuario
//app.MapGet("/get-theme", (HttpContext context) =>
//{
//    var userTheme = context.Session.GetString("userTheme");
//    if (string.IsNullOrEmpty(userTheme))
//    {
//        return Results.NotFound(new { Message = "Tema no establecido para el usuario." });
//    }
//    return Results.Ok(new { Theme = userTheme });
//});

app.Run();