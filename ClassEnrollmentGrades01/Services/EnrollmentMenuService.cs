using System;
using System.Linq;
using ClassEnrollmentGrades01.Models;

namespace ClassEnrollmentGrades01.Services
{
    public class EnrollmentMenuService
    {
        private readonly IDataService _dataService;

        public EnrollmentMenuService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление оценками ===");
                Console.WriteLine("1. Показать все оценки");
                Console.WriteLine("2. Добавить оценку");
                Console.WriteLine("3. Редактировать оценку");
                Console.WriteLine("4. Удалить оценку");
                Console.WriteLine("5. Показать оценки студента");
                Console.WriteLine("0. Назад");
                Console.Write("\nВыберите пункт меню: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ShowAllEnrollments();
                        break;
                    case "2":
                        AddEnrollment();
                        break;
                    case "3":
                        EditEnrollment();
                        break;
                    case "4":
                        DeleteEnrollment();
                        break;
                    case "5":
                        ShowStudentEnrollments();
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

        public void ShowAllEnrollments()
        {
            Console.Clear();
            Console.WriteLine("=== Список всех оценок ===\n");
            var enrollments = _dataService.GetEnrollments()
                .OrderByDescending(e => e.GradeDate)
                .ToList();
            var students = _dataService.GetStudents();
            var courses = _dataService.GetCourses();

            foreach (var enrollment in enrollments)
            {
                var student = students.First(s => s.Id == enrollment.StudentId);
                var course = courses.First(c => c.Id == enrollment.CourseId);
                
                if (enrollment.Grade == 0)
                {
                    Console.WriteLine(
                        $"Студент: {student.LastName} {student.FirstName} (ID: {student.Id}), " +
                        $"Курс: {course.Name} (ID: {course.Id}), " +
                        "Оценка: не выставлена");
                }
                else
                {
                    Console.WriteLine(
                        $"Студент: {student.LastName} {student.FirstName} (ID: {student.Id}), " +
                        $"Курс: {course.Name} (ID: {course.Id}), " +
                        $"Оценка: {enrollment.Grade:F1}, " +
                        $"Дата: {enrollment.GradeDate:dd.MM.yyyy HH:mm}");
                }
            }
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void AddEnrollment()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Выставление оценки ===\n");

                // Показываем всех студентов
                var students = _dataService.GetStudents();
                Console.WriteLine("Список студентов:");
                foreach (var student in students)
                {
                    var enrollments = _dataService.GetEnrollments()
                        .Where(e => e.StudentId == student.Id)
                        .ToList();
                    Console.WriteLine($"ID: {student.Id}, {student.LastName} {student.FirstName}, " +
                                    $"Группа: {student.Group}, Курсов: {enrollments.Count}");
                }

                Console.Write("\nВведите ID студента (или 0 для выхода): ");
                if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId == 0)
                    return;

                var selectedStudent = students.FirstOrDefault(s => s.Id == studentId);
                if (selectedStudent == null)
                {
                    Console.WriteLine("Студент не найден! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                // Показываем все курсы студента
                Console.WriteLine($"\nКурсы студента {selectedStudent.LastName} {selectedStudent.FirstName}:");
                var studentEnrollments = _dataService.GetEnrollments()
                    .Where(e => e.StudentId == studentId)
                    .ToList();
                var courses = _dataService.GetCourses();

                foreach (var course in courses)
                {
                    var enrollment = studentEnrollments.FirstOrDefault(e => e.CourseId == course.Id);
                    if (enrollment != null)
                    {
                        Console.WriteLine(
                            $"ID: {course.Id}, Название: {course.Name}, " +
                            $"Текущая оценка: {(enrollment.Grade == 0 ? "не выставлена" : enrollment.Grade.ToString("F1"))}");
                    }
                    else
                    {
                        Console.WriteLine($"ID: {course.Id}, Название: {course.Name}, Статус: не записан");
                    }
                }

                Console.Write("\nВведите ID курса: ");
                if (!int.TryParse(Console.ReadLine(), out int courseId))
                {
                    Console.WriteLine("Некорректный ID курса! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                var selectedCourse = courses.FirstOrDefault(c => c.Id == courseId);
                if (selectedCourse == null)
                {
                    Console.WriteLine("Курс не найден! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                Console.Write("Введите оценку (от 2.0 до 5.0): ");
                if (!float.TryParse(Console.ReadLine(), out float grade) || grade < 2.0f || grade > 5.0f)
                {
                    Console.WriteLine("Некорректная оценка! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                var existingEnrollment = studentEnrollments.FirstOrDefault(e => e.CourseId == courseId);
                if (existingEnrollment != null)
                {
                    // Обновляем существующую запись
                    _dataService.UpdateEnrollment(new Enrollment 
                    { 
                        StudentId = studentId, 
                        CourseId = courseId, 
                        Grade = grade,
                        GradeDate = DateTime.Now
                    });
                    Console.WriteLine("\nОценка успешно обновлена! Нажмите любую клавишу...");
                }
                else
                {
                    // Создаем новую запись
                    _dataService.AddEnrollment(new Enrollment 
                    { 
                        StudentId = studentId, 
                        CourseId = courseId,
                        Grade = grade,
                        GradeDate = DateTime.Now
                    });
                    Console.WriteLine("\nСтудент записан на курс и выставлена оценка! Нажмите любую клавишу...");
                }
                
                Console.ReadKey();
                return;
            }
        }

        private void EditEnrollment()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Редактирование оценки ===\n");
                
                ShowAllEnrollments();
                
                Console.Write("\nВведите ID студента (или 0 для выхода): ");
                if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId == 0)
                    return;

                var studentEnrollments = _dataService.GetEnrollments()
                    .Where(e => e.StudentId == studentId && e.Grade > 0)
                    .ToList();

                if (!studentEnrollments.Any())
                {
                    Console.WriteLine("У данного студента нет оценок! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                var student = _dataService.GetStudents().First(s => s.Id == studentId);
                Console.WriteLine($"\nОценки студента {student.LastName} {student.FirstName}:");
                foreach (var enrollment in studentEnrollments)
                {
                    var course = _dataService.GetCourses().First(c => c.Id == enrollment.CourseId);
                    Console.WriteLine(
                        $"Курс: {course.Name} (ID: {course.Id}), " +
                        $"Оценка: {enrollment.Grade:F1}, " +
                        $"Дата: {enrollment.GradeDate:dd.MM.yyyy HH:mm}");
                }

                Console.Write("\nВведите ID курса: ");
                if (!int.TryParse(Console.ReadLine(), out int courseId))
                {
                    Console.WriteLine("Некорректный ID курса! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                var existingEnrollment = studentEnrollments.FirstOrDefault(e => e.CourseId == courseId);
                if (existingEnrollment == null)
                {
                    Console.WriteLine("Оценка не найдена! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                Console.Write($"Введите новую оценку (текущая: {existingEnrollment.Grade:F1}): ");
                if (!float.TryParse(Console.ReadLine(), out float grade) || grade < 2.0f || grade > 5.0f)
                {
                    Console.WriteLine("Некорректная оценка! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                _dataService.UpdateEnrollment(new Enrollment 
                { 
                    StudentId = studentId, 
                    CourseId = courseId, 
                    Grade = grade,
                    GradeDate = DateTime.Now
                });
                
                Console.WriteLine("\nОценка успешно обновлена! Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
        }

        private void DeleteEnrollment()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Удаление оценки ===\n");
                
                ShowAllEnrollments();
                
                Console.Write("\nВведите ID студента (или 0 для выхода): ");
                if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId == 0)
                    return;

                var studentEnrollments = _dataService.GetEnrollments()
                    .Where(e => e.StudentId == studentId)
                    .ToList();

                if (!studentEnrollments.Any())
                {
                    Console.WriteLine("У данного студента нет записей на курсы! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                var student = _dataService.GetStudents().First(s => s.Id == studentId);
                Console.WriteLine($"\nЗаписи студента {student.LastName} {student.FirstName}:");
                foreach (var enrollment in studentEnrollments)
                {
                    var course = _dataService.GetCourses().First(c => c.Id == enrollment.CourseId);
                    string gradeInfo = enrollment.Grade == 0 ? "не выставлена" : enrollment.Grade.ToString("F1");
                    Console.WriteLine($"Курс: {course.Name} (ID: {course.Id}), Оценка: {gradeInfo}");
                }

                Console.Write("\nВведите ID курса: ");
                if (!int.TryParse(Console.ReadLine(), out int courseId))
                {
                    Console.WriteLine("Некорректный ID курса! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                if (!studentEnrollments.Any(e => e.CourseId == courseId))
                {
                    Console.WriteLine("Запись не найдена! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                Console.Write("Вы уверены, что хотите удалить запись? (д/н): ");
                if (Console.ReadLine()?.ToLower() == "д")
                {
                    _dataService.DeleteEnrollment(studentId, courseId);
                    Console.WriteLine("\nЗапись успешно удалена! Нажмите любую клавишу...");
                }
                else
                {
                    Console.WriteLine("\nУдаление отменено! Нажмите любую клавишу...");
                }
                Console.ReadKey();
                return;
            }
        }

        private void ShowStudentEnrollments()
        {
            Console.Clear();
            Console.WriteLine("=== Оценки студента ===\n");

            // Показываем список студентов
            var students = _dataService.GetStudents();
            foreach (var studentItem in students)
            {
                Console.WriteLine($"ID: {studentItem.Id}, {studentItem.LastName} {studentItem.FirstName}, Группа: {studentItem.Group}");
            }

            // Запрашиваем ID студента
            Console.Write("\nВведите ID студента (или 0 для выхода): ");
            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId == 0)
                return;

            var student = students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                Console.WriteLine("Студент не найден! Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }

            // Получаем все оценки студента
            var enrollments = _dataService.GetEnrollments()
                .Where(e => e.StudentId == studentId)
                .OrderBy(e => e.Course.Name)
                .ToList();

            Console.Clear();
            Console.WriteLine($"=== Оценки студента: {student.LastName} {student.FirstName} ===");
            Console.WriteLine($"Группа: {student.Group}\n");

            if (enrollments.Any())
            {
                // Подсчет статистики
                var completedCourses = enrollments.Count(e => e.Grade > 0);
                var averageGrade = enrollments.Where(e => e.Grade > 0).Average(e => e.Grade);

                // Вывод оценок
                Console.WriteLine("Оценки по курсам:");
                foreach (var enrollment in enrollments)
                {
                    string gradeInfo = enrollment.Grade == 0 
                        ? "не выставлена" 
                        : $"{enrollment.Grade:F1} (от {enrollment.GradeDate:dd.MM.yyyy HH:mm})";
                        
                    Console.WriteLine($"- {enrollment.Course.Name}: {gradeInfo}");
                }

                // Вывод статистики
                Console.WriteLine("\nСтатистика:");
                Console.WriteLine($"Всего курсов: {enrollments.Count}");
                Console.WriteLine($"Курсов с оценками: {completedCourses}");
                if (completedCourses > 0)
                {
                    Console.WriteLine($"Средний балл: {averageGrade:F2}");
                }
            }
            else
            {
                Console.WriteLine("Студент не записан ни на один курс.");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
} 