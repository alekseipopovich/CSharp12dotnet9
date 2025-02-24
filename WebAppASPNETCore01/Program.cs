using WebAppASPNETCore01.Controllers;
using WebAppASPNETCore01.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var studentsList = new List<Student>()
            {
                new Student { Id = 1, FirstName = "����", LastName = "������", Email = "ivan@example.com" },
                new Student { Id = 2, FirstName = "�����", LastName = "�������", Email = "maria@example.com" },
                new Student { Id = 3, FirstName = "�������", LastName = "�������", Email = "alex@example.com" },
                new Student { Id = 4, FirstName = "�����", LastName = "�������", Email = "elena@example.com" },
                new Student { Id = 5, FirstName = "�������", LastName = "�������", Email = "dmitry@example.com" }
            };
StudentController.InitializeData(studentsList);

app.Run();
