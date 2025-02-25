using System;
using System.Linq;
using ClassEnrollmentGrades01.Models;

namespace ClassEnrollmentGrades01.Services
{
    public class StudentMenuService
    {
        private readonly IDataService _dataService;

        public StudentMenuService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление студентами ===");
                Console.WriteLine("1. Показать всех студентов");
                Console.WriteLine("2. Добавить студента");
                Console.WriteLine("3. Редактировать студента");
                Console.WriteLine("4. Удалить студента");
                Console.WriteLine("5. Записать студента на курс");
                Console.WriteLine("0. Назад");
                Console.Write("\nВыберите пункт меню: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ShowAllStudents();
                        break;
                    case "2":
                        AddStudent();
                        break;
                    case "3":
                        EditStudent();
                        break;
                    case "4":
                        DeleteStudent();
                        break;
                    case "5":
                        EnrollStudentToCourse();
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

        public void ShowAllStudents()
        {
            Console.Clear();
            Console.WriteLine("=== Список всех студентов ===\n");
            var students = _dataService.GetStudents();
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, {student.LastName} {student.FirstName}, Группа: {student.Group}");
            }
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Добавление нового студента ===\n");
            
            Console.Write("Введите имя студента: ");
            string firstName;
            do
            {
                firstName = Console.ReadLine() ?? string.Empty;
            } while (string.IsNullOrWhiteSpace(firstName));
            
            Console.Write("Введите фамилию студента: ");
            string lastName;
            do
            {
                lastName = Console.ReadLine() ?? string.Empty;
            } while (string.IsNullOrWhiteSpace(lastName));
            
            Console.Write("Введите группу студента: ");
            string group;
            do
            {
                group = Console.ReadLine() ?? string.Empty;
            } while (string.IsNullOrWhiteSpace(group));

            _dataService.AddStudent(new Student 
            { 
                FirstName = firstName.Trim(), 
                LastName = lastName.Trim(), 
                Group = group.Trim() 
            });
            
            Console.WriteLine("\nСтудент успешно добавлен! Нажмите любую клавишу...");
            Console.ReadKey();
        }

        private void EditStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Редактирование студента ===\n");
            
            ShowAllStudents();
            
            Console.Write("\nВведите ID студента для редактирования: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var student = _dataService.GetStudents().FirstOrDefault(s => s.Id == id);
                if (student != null)
                {
                    Console.Write("Введите новое имя студента: ");
                    string input = Console.ReadLine() ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        student.FirstName = input.Trim();
                    }
                    
                    Console.Write("Введите новую фамилию студента: ");
                    input = Console.ReadLine() ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        student.LastName = input.Trim();
                    }
                    
                    Console.Write("Введите новую группу студента: ");
                    input = Console.ReadLine() ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        student.Group = input.Trim();
                    }

                    _dataService.UpdateStudent(student);
                    Console.WriteLine("\nДанные студента успешно обновлены!");
                }
                else
                {
                    Console.WriteLine("\nСтудент не найден!");
                }
            }
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private void DeleteStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Удаление студента ===\n");
            
            ShowAllStudents();
            
            Console.Write("\nВведите ID студента для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _dataService.DeleteStudent(id);
                Console.WriteLine("\nСтудент успешно удален!");
            }
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private void EnrollStudentToCourse()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Запись студента на курс ===\n");

                ShowAllStudents();
                Console.Write("\nВведите ID студента (или 0 для выхода): ");
                if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId == 0)
                    return;

                var student = _dataService.GetStudents().FirstOrDefault(s => s.Id == studentId);
                if (student == null)
                {
                    Console.WriteLine("Студент не найден! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                // Показываем курсы, на которые студент еще не записан
                var existingEnrollments = _dataService.GetEnrollments()
                    .Where(e => e.StudentId == studentId)
                    .Select(e => e.CourseId);
                
                var availableCourses = _dataService.GetCourses()
                    .Where(c => !existingEnrollments.Contains(c.Id))
                    .ToList();

                if (!availableCourses.Any())
                {
                    Console.WriteLine("Студент уже записан на все доступные курсы! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine("\nДоступные курсы:");
                foreach (var course in availableCourses)
                {
                    Console.WriteLine($"ID: {course.Id}, Название: {course.Name}, Описание: {course.Description}");
                }

                Console.Write("\nВведите ID курса: ");
                if (!int.TryParse(Console.ReadLine(), out int courseId))
                {
                    Console.WriteLine("Некорректный ID курса! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                if (!availableCourses.Any(c => c.Id == courseId))
                {
                    Console.WriteLine("Курс не найден или студент уже записан! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                _dataService.AddEnrollment(new Enrollment 
                { 
                    StudentId = studentId, 
                    CourseId = courseId,
                    Grade = 0,
                    GradeDate = DateTime.MinValue
                });
                
                Console.WriteLine("\nСтудент успешно записан на курс! Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
        }
    }
} 