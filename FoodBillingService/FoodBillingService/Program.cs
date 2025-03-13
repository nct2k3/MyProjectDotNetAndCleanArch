using FoodBillingService.Controller;
using FoodBillingService.Infrastructure;
using FoodBillingService.Service.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String)); 

builder.Services.AddInfrastructure();
builder.Services.AddHostedService<RabbitMQBackgroundConsumer>();
builder.Services.AddScoped<OrderConsumer>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.Run();