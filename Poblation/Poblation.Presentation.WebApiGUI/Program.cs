using Microsoft.EntityFrameworkCore;
using Population.Infraestructure.Contracts;
using Population.Infraestructure.Impl;
using Population.Infraestructure.Impl.DbContexts;
using Population.Library.Contracts;
using Population.Library.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddScoped<IPopulationService, PopulationService>()
    .AddScoped<ICountryRepository, CountryRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PopulationDBContext>(options => options.UseSqlServer(builder.Configuration["PopulationDB"]));

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
