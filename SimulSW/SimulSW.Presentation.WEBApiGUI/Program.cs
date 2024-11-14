using Microsoft.EntityFrameworkCore;
using SimulSW.Infraestructure.Contracts;
using SimulSW.Infraestructure.Impl;
using SimulSW.Infraestructure.Impl.DBContexts;
using SimulSW.Library.Contracts;
using SimulSW.Library.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddScoped<IAppService, AppService>()
    .AddScoped<IPlanetRepository, PlanetRepository>()
    .AddScoped<ISWApiRepository, SWApiRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SWDBContext>(options => options.UseSqlServer(builder.Configuration["SWDB"]));

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
