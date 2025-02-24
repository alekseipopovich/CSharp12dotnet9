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
                new Student { Id = 1, FirstName = "Иван", LastName = "Петров", Email = "ivan@example.com" },
                new Student { Id = 2, FirstName = "Мария", LastName = "Иванова", Email = "maria@example.com" },
                new Student { Id = 3, FirstName = "Алексей", LastName = "Смирнов", Email = "alex@example.com" },
                new Student { Id = 4, FirstName = "Елена", LastName = "Козлова", Email = "elena@example.com" },
                new Student { Id = 5, FirstName = "Дмитрий", LastName = "Соколов", Email = "dmitry@example.com" }
            };
StudentController.InitializeData(studentsList);

app.Run();
