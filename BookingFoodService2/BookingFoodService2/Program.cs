using BookingFoodService2.Controller;
using BookingFoodService2.Infrastructure;
using BookingFoodServie2.Controller;
using BookingFoodServie2.Data;
using BookingFoodServie2.Service;
using BookingFoodServie2.Service.Comands;
using BookingFoodServie2.Service.Mapping;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<RabbitMQBackgroundConsumer>();

builder.Services.AddControllers();
builder.Services.AddInfrastructure();
builder.Services.AddService();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
