
using BockingFood;
using BockingFood.MiddleWare;
using Infrastructure;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Presentation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);






var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
// cac middleware cehck 
app.UseMiddleware<ErrorHandlingMiddleware>();

// Một phương án bắt lỗi dự phòng khi middleware và filters không xử lý tới
app.UseExceptionHandler("/error");
// có thể k cần thiết nếu đã xư lý tốt chị là lốp dự phòng 
//
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
