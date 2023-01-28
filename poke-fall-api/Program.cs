
using Microsoft.EntityFrameworkCore;
using poke_fall_api.Models;

var builder = WebApplication.CreateBuilder(args);

/** 
* Process to update schema
* 1) run 'dotnet ef migrations add <migration_name>'
* 2) run 'dotnet ef database update'
*/
builder.Services.AddDbContext<poke_fall_api.Models.PokefallContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Pokefall"), 
                ));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
