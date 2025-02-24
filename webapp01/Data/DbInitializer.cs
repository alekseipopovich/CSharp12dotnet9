using webapp01.Models;

namespace webapp01.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Убедимся, что база данных создана
        context.Database.EnsureCreated();

        // Проверяем, есть ли уже курсы
        if (context.Courses.Any())
        {
            return; // База данных уже заполнена
        }

        // Добавляем курсы
        var courses = new Course[]
        {
            new Course { Name = "Математический анализ", Description = "Основы математического анализа" },
            new Course { Name = "Программирование на C#", Description = "Изучение языка C# и .NET платформы" },
            new Course { Name = "Базы данных", Description = "Основы проектирования баз данных" },
            new Course { Name = "Web-разработка", Description = "Разработка веб-приложений" }
        };
        context.Courses.AddRange(courses);
        context.SaveChanges();

        // Добавляем студентов
        var students = new Student[]
        {
            new Student { FirstName = "Иван", LastName = "Иванов", Email = "ivan@example.com" },
            new Student { FirstName = "Мария", LastName = "Петрова", Email = "maria@example.com" },
            new Student { FirstName = "Алексей", LastName = "Сидоров", Email = "alex@example.com" },
            new Student { FirstName = "Елена", LastName = "Козлова", Email = "elena@example.com" }
        };
        context.Students.AddRange(students);
        context.SaveChanges();

        // Добавляем оценки
        var studentCourses = new StudentCourse[]
        {
            new StudentCourse { StudentId = students[0].Id, CourseId = courses[0].Id, Grade = 85 },
            new StudentCourse { StudentId = students[0].Id, CourseId = courses[1].Id, Grade = 92 },
            new StudentCourse { StudentId = students[1].Id, CourseId = courses[0].Id, Grade = 78 },
            new StudentCourse { StudentId = students[1].Id, CourseId = courses[2].Id, Grade = 88 },
            new StudentCourse { StudentId = students[2].Id, CourseId = courses[1].Id, Grade = 95 },
            new StudentCourse { StudentId = students[2].Id, CourseId = courses[3].Id, Grade = 90 },
            new StudentCourse { StudentId = students[3].Id, CourseId = courses[2].Id, Grade = 87 },
            new StudentCourse { StudentId = students[3].Id, CourseId = courses[3].Id, Grade = 83 }
        };
        context.StudentCourses.AddRange(studentCourses);
        context.SaveChanges();
    }
} 