global using dotnet_rpg.Models; // In C# 11, the global using directive allows you to import a namespace globally across your entire project. It simplifies and reduces the number of using directives you need to include in each file.
global using dotnet_rpg.Services.CharacterService;
global using dotnet_rpg.DTOs.Character;
global using dotnet_rpg.DTOs.User;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using DotNetEnv;
global using dotnet_rpg.Data;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

Env.Load(); // Load the .env file

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => 
    options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly); // This is the assembly where the mapping profiles are located (in this case, the Program.cs file)
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // The AddAuthentication method adds the authentication services to the DI container. It configures the default scheme that will be used by the Authorize attribute.
    .AddJwtBearer(options => // The AddJwtBearer method configures the JwtBearer authentication scheme. It tells the application how to validate JWT tokens that are passed to it. The JwtBearer scheme is the default scheme for ASP.NET Core applications.
    {
        options.TokenValidationParameters = new TokenValidationParameters // The TokenValidationParameters class is used to configure the validation of the token. The ValidateIssuerSigningKey property is set to true to indicate that the token will be validated using a signing key. The IssuerSigningKey property contains the security key that is used to validate the token. The ValidateIssuer and ValidateAudience properties indicate that the issuer and audience will not be validated. In a real-world application, you would want to validate these values.
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TOKEN")!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // The UseAuthentication method adds the authentication middleware to the HTTP request pipeline. It tells the application to use authentication when processing requests. The UseAuthentication method must appear between the UseRouting and UseEndpoints methods.

app.UseAuthorization();

app.MapControllers();

app.Run();
