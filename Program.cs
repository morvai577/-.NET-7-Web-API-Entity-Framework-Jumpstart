global using dotnet_rpg.Models; // In C# 11, the global using directive allows you to import a namespace globally across your entire project. It simplifies and reduces the number of using directives you need to include in each file.
global using dotnet_rpg.Services.CharacterService;
global using dotnet_rpg.DTOs.Character;
global using dotnet_rpg.DTOs.User;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using DotNetEnv;
global using dotnet_rpg.Data;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
