using DnD_API.Data;
using DnD_API.Services;
using DnD_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

//configs
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//InMemory DI
builder.Services.AddScoped<ICharacterServices, CharacterService>();
builder.Services.AddScoped<RunService>();
builder.Services.AddScoped<IDiceService, DiceService>();
builder.Services.AddScoped<IRunStore, RunStore>();
builder.Services.AddScoped<DungeonService>();
//InMemory Db Setup... 
builder.Services.AddDbContext<DnDDbContext>(options => options.UseInMemoryDatabase("DnDDbContext"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();