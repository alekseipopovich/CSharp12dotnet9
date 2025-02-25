using System.Collections.Generic;
using System.Threading.Tasks;
using ClassEnrollmentGrades01.Models;

namespace ClassEnrollmentGrades01.Services
{
    public interface IDataService
    {
        // Синхронные методы для обратной совместимости
        List<Course> GetCourses();
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);

        List<Student> GetStudents();
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int id);

        List<Enrollment> GetEnrollments();
        void AddEnrollment(Enrollment enrollment);
        void UpdateEnrollment(Enrollment enrollment);
        void DeleteEnrollment(int studentId, int courseId);

        // Асинхронные методы
        Task<List<Course>> GetCoursesAsync();
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);

        Task<List<Student>> GetStudentsAsync();
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);

        Task<List<Enrollment>> GetEnrollmentsAsync();
        Task AddEnrollmentAsync(Enrollment enrollment);
        Task UpdateEnrollmentAsync(Enrollment enrollment);
        Task DeleteEnrollmentAsync(int studentId, int courseId);
    }
} 