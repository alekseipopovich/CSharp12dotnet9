using System.Collections.Generic;
using System.Threading.Tasks;
using ClassEnrollmentGrades01.Services.Database;
using ClassEnrollmentGrades01.Models;

namespace ClassEnrollmentGrades01.Services
{
    public class DbDataService : IDataService, IDisposable
    {
        private readonly CourseDbService _courseService;
        private readonly StudentDbService _studentService;
        private readonly EnrollmentDbService _enrollmentService;
        private readonly DbInitializer _initializer;

        public DbDataService()
        {
            _courseService = new CourseDbService();
            _studentService = new StudentDbService();
            _enrollmentService = new EnrollmentDbService();
            _initializer = new DbInitializer();
        }

        public void SeedInitialData() => _initializer.InitializeDatabase();

        // Синхронные методы
        public List<Course> GetCourses() => _courseService.GetCourses();
        public void AddCourse(Course course) => _courseService.AddCourse(course);
        public void UpdateCourse(Course course) => _courseService.UpdateCourse(course);
        public void DeleteCourse(int id) => _courseService.DeleteCourse(id);

        public List<Student> GetStudents() => _studentService.GetStudents();
        public void AddStudent(Student student) => _studentService.AddStudent(student);
        public void UpdateStudent(Student student) => _studentService.UpdateStudent(student);
        public void DeleteStudent(int id) => _studentService.DeleteStudent(id);

        public List<Enrollment> GetEnrollments() => _enrollmentService.GetEnrollments();
        public void AddEnrollment(Enrollment enrollment) => _enrollmentService.AddEnrollment(enrollment);
        public void UpdateEnrollment(Enrollment enrollment) => _enrollmentService.UpdateEnrollment(enrollment);
        public void DeleteEnrollment(int studentId, int courseId) => _enrollmentService.DeleteEnrollment(studentId, courseId);

        // Асинхронные методы
        public Task<List<Course>> GetCoursesAsync() => Task.FromResult(GetCourses());
        public Task AddCourseAsync(Course course) { AddCourse(course); return Task.CompletedTask; }
        public Task UpdateCourseAsync(Course course) { UpdateCourse(course); return Task.CompletedTask; }
        public Task DeleteCourseAsync(int id) { DeleteCourse(id); return Task.CompletedTask; }

        public Task<List<Student>> GetStudentsAsync() => Task.FromResult(GetStudents());
        public Task AddStudentAsync(Student student) { AddStudent(student); return Task.CompletedTask; }
        public Task UpdateStudentAsync(Student student) { UpdateStudent(student); return Task.CompletedTask; }
        public Task DeleteStudentAsync(int id) { DeleteStudent(id); return Task.CompletedTask; }

        public Task<List<Enrollment>> GetEnrollmentsAsync() => Task.FromResult(GetEnrollments());
        public Task AddEnrollmentAsync(Enrollment enrollment) { AddEnrollment(enrollment); return Task.CompletedTask; }
        public Task UpdateEnrollmentAsync(Enrollment enrollment) { UpdateEnrollment(enrollment); return Task.CompletedTask; }
        public Task DeleteEnrollmentAsync(int studentId, int courseId) { DeleteEnrollment(studentId, courseId); return Task.CompletedTask; }

        public void Dispose()
        {
            _courseService.Dispose();
            _studentService.Dispose();
            _enrollmentService.Dispose();
            _initializer.Dispose();
        }
    }
} 