using Microsoft.EntityFrameworkCore;
using Universities.Infraestructure.Contracts;
using Universities.Infraestructure.Impl;
using Universities.Infraestructure.Impl.DBContext;
using Universities.Library.Contracts;
using Universities.Library.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddScoped<IAppService, AppService>()
    .AddScoped<IAPIRepository, APIRepository>()
    .AddScoped<IDBUniversityRepository, DBUniversityRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SimulUnivContext>(options => options.UseSqlServer(builder.Configuration["SimulUnivDB"]));

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
