
using Microsoft.EntityFrameworkCore;
using poke_fall_api.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

/** 
* Process to update schema
* 1) run 'dotnet ef migrations add <migration_name>'
* 2) run 'dotnet ef database update'
*/
builder.Services.AddDbContext<poke_fall_api.Models.PokefallContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Pokefall")));
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().WithMethods("GET");
                      });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
