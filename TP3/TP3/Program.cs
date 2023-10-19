using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TP3.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TP3Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TP3Context") ?? throw new InvalidOperationException("Connection string 'TP3Context' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
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

app.UseRouting();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
