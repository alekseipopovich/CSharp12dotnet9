using System;
using System.Linq;
using ClassEnrollmentGrades01.Models;

namespace ClassEnrollmentGrades01.Services
{
    public class CourseMenuService
    {
        private readonly IDataService _dataService;

        public CourseMenuService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление курсами ===");
                Console.WriteLine("1. Показать все курсы");
                Console.WriteLine("2. Добавить курс");
                Console.WriteLine("3. Редактировать курс");
                Console.WriteLine("4. Удалить курс");
                Console.WriteLine("5. Показать студентов по курсам");
                Console.WriteLine("0. Назад");
                Console.Write("\nВыберите пункт меню: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ShowAllCourses();
                        break;
                    case "2":
                        AddCourse();
                        break;
                    case "3":
                        EditCourse();
                        break;
                    case "4":
                        DeleteCourse();
                        break;
                    case "5":
                        ShowStudentsByCourses();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public void ShowAllCourses()
        {
            Console.Clear();
            Console.WriteLine("=== Список всех курсов ===\n");
            var courses = _dataService.GetCourses();
            foreach (var course in courses)
            {
                Console.WriteLine($"ID: {course.Id}, Название: {course.Name}, Описание: {course.Description}");
            }
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void AddCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Добавление нового курса ===\n");
            
            Console.Write("Введите название курса: ");
            string name;
            do
            {
                name = Console.ReadLine() ?? string.Empty;
            } while (string.IsNullOrWhiteSpace(name));
            
            Console.Write("Введите описание курса: ");
            string description;
            do
            {
                description = Console.ReadLine() ?? string.Empty;
            } while (string.IsNullOrWhiteSpace(description));

            _dataService.AddCourse(new Course 
            { 
                Name = name.Trim(), 
                Description = description.Trim() 
            });
            
            Console.WriteLine("\nКурс успешно добавлен! Нажмите любую клавишу...");
            Console.ReadKey();
        }

        private void EditCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Редактирование курса ===\n");
            
            ShowAllCourses();
            
            Console.Write("\nВведите ID курса для редактирования: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var course = _dataService.GetCourses().FirstOrDefault(c => c.Id == id);
                if (course != null)
                {
                    Console.Write("Введите новое название курса: ");
                    string input = Console.ReadLine() ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        course.Name = input.Trim();
                    }
                    
                    Console.Write("Введите новое описание курса: ");
                    input = Console.ReadLine() ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        course.Description = input.Trim();
                    }

                    _dataService.UpdateCourse(course);
                    Console.WriteLine("\nКурс успешно обновлен!");
                }
                else
                {
                    Console.WriteLine("\nКурс не найден!");
                }
            }
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private void DeleteCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Удаление курса ===\n");
            
            ShowAllCourses();
            
            Console.Write("\nВведите ID курса для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _dataService.DeleteCourse(id);
                Console.WriteLine("\nКурс успешно удален!");
            }
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private void ShowStudentsByCourses()
        {
            Console.Clear();
            Console.WriteLine("=== Список студентов по курсам ===\n");

            var courses = _dataService.GetCourses();
            var enrollments = _dataService.GetEnrollments();
            var students = _dataService.GetStudents();

            foreach (var course in courses)
            {
                Console.WriteLine($"\nКурс: {course.Name} (ID: {course.Id})");
                Console.WriteLine("Описание: " + course.Description);
                
                var courseEnrollments = enrollments
                    .Where(e => e.CourseId == course.Id)
                    .OrderBy(e => students.First(s => s.Id == e.StudentId).LastName)
                    .ThenBy(e => students.First(s => s.Id == e.StudentId).FirstName)
                    .ToList();

                if (courseEnrollments.Any())
                {
                    Console.WriteLine("Студенты:");
                    foreach (var enrollment in courseEnrollments)
                    {
                        var student = students.First(s => s.Id == enrollment.StudentId);
                        string gradeInfo = enrollment.Grade == 0 
                            ? "оценка не выставлена" 
                            : $"оценка: {enrollment.Grade:F1}";
                            
                        Console.WriteLine($"  - {student.LastName} {student.FirstName}, " +
                                        $"группа: {student.Group}, {gradeInfo}" +
                                        (enrollment.Grade > 0 ? $", дата: {enrollment.GradeDate:dd.MM.yyyy HH:mm}" : ""));
                    }
                }
                else
                {
                    Console.WriteLine("На данный курс пока никто не записан");
                }
                Console.WriteLine(new string('-', 50));
            }

            Console.WriteLine("\nСтатистика по курсам:");
            foreach (var course in courses)
            {
                var courseEnrollments = enrollments.Where(e => e.CourseId == course.Id).ToList();
                var studentsCount = courseEnrollments.Count;
                var gradedStudentsCount = courseEnrollments.Count(e => e.Grade > 0);
                var averageGrade = courseEnrollments.Where(e => e.Grade > 0).Select(e => e.Grade).DefaultIfEmpty(0).Average();

                Console.WriteLine($"{course.Name}:");
                Console.WriteLine($"  Всего студентов: {studentsCount}");
                Console.WriteLine($"  Получили оценки: {gradedStudentsCount}");
                if (averageGrade > 0)
                {
                    Console.WriteLine($"  Средний балл: {averageGrade:F2}");
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
} 