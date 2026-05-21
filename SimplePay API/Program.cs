using Microsoft.EntityFrameworkCore;
using SimplePay_API.Context;
using System.Reflection.Emit;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opt => 
    opt.JsonSerializerOptions
        .ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opts => 
        opts.UseMySql(connString, ServerVersion.AutoDetect(connString))
);


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
