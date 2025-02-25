using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassEnrollmentGrades01.Models;

namespace ClassEnrollmentGrades01.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }
        
        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }
        
        [Required]
        [MaxLength(20)]
        public required string Group { get; set; }
    }
} 

public interface IDataService
{
    // Асинхронные методы для работы с курсами
    Task<List<Course>> GetCoursesAsync();
    Task AddCourseAsync(Course course);
    Task UpdateCourseAsync(Course course);
    Task DeleteCourseAsync(int id);

    // Асинхронные методы для работы со студентами
    Task<List<Student>> GetStudentsAsync();
    Task AddStudentAsync(Student student);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int id);

    // Асинхронные методы для работы с оценками
    Task<List<Enrollment>> GetEnrollmentsAsync();
    Task AddEnrollmentAsync(Enrollment enrollment);
    Task UpdateEnrollmentAsync(Enrollment enrollment);
    Task DeleteEnrollmentAsync(int studentId, int courseId);
}