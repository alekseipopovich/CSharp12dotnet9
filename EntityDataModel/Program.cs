using EntityDataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace EntityDataModel;
internal class Program
{
    private static void Main(string[] args)
    {
        using (AppContext db = new())
        {

            //Student s1 = db.Students.FirstOrDefault(c => c.FirstName == "Vasya" && c.LastName == "Pupkin")
            //    ?? new Student { StudentId = 1, FirstName = "Vasya", LastName = "Pupkin", Age = 20, Address = "Moskow" };
            
            Student s1 = new Student { StudentId = 1, FirstName = "Vasya", LastName = "Pupkin", Age = 20, Address = "Moskow" };
                
            Student s2 = new Student { StudentId = 2, FirstName = "Ivan", LastName = "Ivanov", Age = 25, Address = "Bratsk" };

            db.Students.Add(s1); db.Students.Add(s2);

            Course course1 = new Course { CourseId = 1, CourseName = "Математика" };
            Course course2 = new Course { CourseId = 2, CourseName = "Физика" };

            db.Courses.Add(course1); db.Courses.Add(course2);
            
            var grade1 = db.Grades.Add(new Grade { GradeID = 1, Student = s1, Course = course1, Score = 4 });

            var grade2 = db.Grades.Add(new Grade { GradeID = 2, Student = s2, Course = course2, Score = 3 });

            try
            {
                db.SaveChanges();
            } catch ( DbUpdateException ex)
            {
                Console.WriteLine("Проблемы с обновлением данных");
            }

            var allStudents = db.Students.ToList();
            Console.WriteLine("Список студентов:");
            foreach (var student in allStudents)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}");
            }

            // Запрос 2: Получить оценки студента по имени "Иван"
            var ivanGrades = db.Grades
                .Where(g => g.Student.FirstName == "Ivan")
                .Select(g => new
                {
                    CourseName = g.Course.CourseName,
                    Score = g.Score
                })
                .ToList();

            Console.WriteLine("\nОценки Ивана:");
            foreach (var grade in ivanGrades)
            {
                Console.WriteLine($"{grade.CourseName}: {grade.Score}");
            }

            // Запрос 3: Получить средний балл по каждому курсу
            var averageScores = db.Grades
                .GroupBy(g => g.Course.CourseName)
                .Select(g => new
                {
                    CourseName = g.Key,
                    AverageScore = g.Average(gr => gr.Score)
                })
                .ToList();

            Console.WriteLine("\nСредний балл по курсам:");
            foreach (var item in averageScores)
            {
                Console.WriteLine($"{item.CourseName}: {item.AverageScore:F2}");
            }
        }
    }
}