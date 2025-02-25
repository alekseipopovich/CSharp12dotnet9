using Microsoft.EntityFrameworkCore;
using ClassEnrollmentGrades01.Models;

namespace ClassEnrollmentGrades01.Services.Database
{
    public class EnrollmentDbService : DbServiceBase
    {
        public List<Enrollment> GetEnrollments()
        {
            var sql = @"
                SELECT 
                    e.StudentId,
                    e.CourseId,
                    e.Grade,
                    e.GradeDate,
                    s.FirstName,
                    s.LastName,
                    s.[Group],
                    c.Name as CourseName,
                    c.Description as CourseDescription
                FROM Enrollments e
                JOIN Students s ON e.StudentId = s.Id
                JOIN Courses c ON e.CourseId = c.Id
                ORDER BY e.GradeDate DESC";

            var enrollments = new List<Enrollment>();
            
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _context.Database.OpenConnection();
                
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        enrollments.Add(CreateEnrollmentFromReader(result));
                    }
                }
            }
            
            return enrollments;
        }

        private Enrollment CreateEnrollmentFromReader(System.Data.Common.DbDataReader result)
        {
            return new Enrollment
            {
                StudentId = result.GetInt32(0),
                CourseId = result.GetInt32(1),
                Grade = (float)result.GetDouble(2),
                GradeDate = result.GetDateTime(3),
                Student = new Student
                {
                    Id = result.GetInt32(0),
                    FirstName = result.GetString(4),
                    LastName = result.GetString(5),
                    Group = result.GetString(6)
                },
                Course = new Course
                {
                    Id = result.GetInt32(1),
                    Name = result.GetString(7),
                    Description = result.GetString(8)
                }
            };
        }

        public void AddEnrollment(Enrollment enrollment)
        {
            try
            {
                _context.Database.ExecuteSqlRaw(
                    "INSERT INTO Enrollments (StudentId, CourseId, Grade, GradeDate) VALUES (@p0, @p1, @p2, @p3)",
                    enrollment.StudentId, 
                    enrollment.CourseId,
                    enrollment.Grade,
                    enrollment.GradeDate);
                    
                _context.ChangeTracker.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении записи: {ex.Message}");
                throw;
            }
        }

        public void UpdateEnrollment(Enrollment enrollment)
        {
            try
            {
                _context.Database.ExecuteSqlRaw(
                    "UPDATE Enrollments SET Grade = @p0, GradeDate = @p1 WHERE StudentId = @p2 AND CourseId = @p3",
                    enrollment.Grade,
                    enrollment.GradeDate,
                    enrollment.StudentId,
                    enrollment.CourseId);
                    
                _context.ChangeTracker.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении записи: {ex.Message}");
                throw;
            }
        }

        public void DeleteEnrollment(int studentId, int courseId)
        {
            try
            {
                _context.Database.ExecuteSqlRaw(
                    "DELETE FROM Enrollments WHERE StudentId = @p0 AND CourseId = @p1",
                    studentId, courseId);
                
                _context.ChangeTracker.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении записи: {ex.Message}");
                throw;
            }
        }
    }
} 