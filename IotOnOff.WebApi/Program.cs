using Devices.Abstract.OnOff;
using Devices.Factory;
using Devices.Factory.Devices;
using IoTOnOff.WebApi.Servicios;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IOnOffDevice>(new OnOffTcpIpFactory().Create());
builder.Services.AddSingleton<WebSocketOnOffService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(
    c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
        c.RoutePrefix = String.Empty;
    });


app.UseCors();

app.UseWebSockets();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

