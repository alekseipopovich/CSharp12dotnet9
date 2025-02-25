using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ClassEnrollmentGrades01.Models;

namespace ClassEnrollmentGrades01.Services
{
    public class StatisticsService
    {
        private readonly IDataService _dataService;

        public StatisticsService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task ShowCoursesStatisticsAsync()
        {
            var courses = await _dataService.GetCoursesAsync();
            var enrollments = await _dataService.GetEnrollmentsAsync();

            var statistics = await Task.WhenAll(courses.Select(async course =>
            {
                var courseEnrollments = enrollments.Where(e => e.CourseId == course.Id).ToList();
                return new
                {
                    Course = course,
                    StudentsCount = courseEnrollments.Count(),
                    GradedCount = courseEnrollments.Count(e => e.Grade > 0),
                    AverageGrade = courseEnrollments.Where(e => e.Grade > 0)
                                                  .Select(e => e.Grade)
                                                  .DefaultIfEmpty(0)
                                                  .Average()
                };
            }));

            foreach (var stat in statistics)
            {
                Console.WriteLine($"Курс: {stat.Course.Name}");
                Console.WriteLine($"  Всего студентов: {stat.StudentsCount}");
                Console.WriteLine($"  Получили оценки: {stat.GradedCount}");
                if (stat.AverageGrade > 0)
                    Console.WriteLine($"  Средний балл: {stat.AverageGrade:F2}");
            }
        }

        public async Task ShowStudentDetailsAsync(int studentId)
        {
            var student = await _dataService.GetStudentsAsync()
                .ContinueWith(t => t.Result.FirstOrDefault(s => s.Id == studentId));
            var enrollments = await _dataService.GetEnrollmentsAsync()
                .ContinueWith(t => t.Result.Where(e => e.StudentId == studentId).ToList());

            if (student == null)
            {
                Console.WriteLine("Студент не найден");
                return;
            }

            Console.WriteLine($"Студент: {student.LastName} {student.FirstName}");
            Console.WriteLine($"Группа: {student.Group}");
            
            foreach (var enrollment in enrollments)
            {
                var course = await _dataService.GetCoursesAsync()
                    .ContinueWith(t => t.Result.First(c => c.Id == enrollment.CourseId));
                    
                Console.WriteLine($"Курс: {course.Name}, Оценка: {enrollment.Grade:F1}");
            }
        }
    }

    public class BackgroundStatisticsService : BackgroundService
    {
        private readonly IDataService _dataService;
        private readonly ILogger<BackgroundStatisticsService> _logger;

        public BackgroundStatisticsService(
            IDataService dataService,
            ILogger<BackgroundStatisticsService> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CalculateAndSaveStatisticsAsync();
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при расчете статистики");
                }
            }
        }

        private async Task CalculateAndSaveStatisticsAsync()
        {
            var courses = await _dataService.GetCoursesAsync();
            var enrollments = await _dataService.GetEnrollmentsAsync();

            foreach (var course in courses)
            {
                var courseEnrollments = enrollments.Where(e => e.CourseId == course.Id).ToList();
                var stats = new CourseStatistics
                {
                    CourseId = course.Id,
                    StudentsCount = courseEnrollments.Count,
                    GradedCount = courseEnrollments.Count(e => e.Grade > 0),
                    AverageGrade = courseEnrollments.Where(e => e.Grade > 0)
                        .Select(e => e.Grade)
                        .DefaultIfEmpty(0)
                        .Average()
                };
                
                // TODO: Сохранение статистики в базу данных
            }
        }
    }

    public class CourseStatistics
    {
        public int CourseId { get; set; }
        public int StudentsCount { get; set; }
        public int GradedCount { get; set; }
        public float AverageGrade { get; set; }
    }
} 