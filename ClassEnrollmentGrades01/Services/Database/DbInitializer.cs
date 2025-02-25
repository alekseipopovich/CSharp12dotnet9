using Microsoft.EntityFrameworkCore;
using ClassEnrollmentGrades01.Data;

namespace ClassEnrollmentGrades01.Services.Database
{
    public class DbInitializer : DbServiceBase
    {
        public void InitializeDatabase()
        {
            try
            {
                (_context as ApplicationContext)?.InitializeDatabase();
                SeedInitialData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при инициализации базы данных: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Внутренняя ошибка: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        private void SeedInitialData()
        {
            // Добавляем курсы
            _context.Database.ExecuteSqlRaw(@"
                INSERT INTO Courses (Name, Description) VALUES 
                ('Математика', 'Высшая математика'),
                ('Физика', 'Общая физика'),
                ('Программирование', 'Основы C#')
            ");

            // Добавляем студентов
            _context.Database.ExecuteSqlRaw(@"
                INSERT INTO Students (FirstName, LastName, [Group]) VALUES 
                ('Иван', 'Петров', '101'),
                ('Мария', 'Иванова', '102'),
                ('Алексей', 'Сидоров', '101')
            ");

            // Добавляем оценки
            _context.Database.ExecuteSqlRaw(@"
                INSERT INTO Enrollments (StudentId, CourseId, Grade, GradeDate) VALUES 
                (1, 1, 4.5, datetime('now', '-5 days')),
                (1, 2, 4.0, datetime('now', '-4 days')),
                (2, 1, 5.0, datetime('now', '-3 days')),
                (2, 3, 4.8, datetime('now', '-2 days')),
                (3, 2, 3.5, datetime('now', '-1 days')),
                (3, 3, 4.2, datetime('now'))
            ");

            _context.ChangeTracker.Clear();
        }
    }
} 