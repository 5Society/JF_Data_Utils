using API_JF_Data_Utils_Example.Core.Interfaces;
using API_JF_Data_Utils_Example.Core.Services;
using API_JF_Data_Utils_Example.DataAccess;
using API_JF_Data_Utils_Example.DataAccess.Interfaces;
using API_JF_Data_Utils_Example.DataAccess.Repositories;
using JF.Utils.Data;
using JF.Utils.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add dbContext
builder.Services.AddDbContext<JFContext>(options => { options.UseInMemoryDatabase("Test"); });
// Add Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ISalonService, SalonService>();
// Add Repositories
builder.Services.AddScoped<ISalonRepository, SalonRepository>();
builder.Services.AddScoped<IUnitOfWork, ApplicationContext>();

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
