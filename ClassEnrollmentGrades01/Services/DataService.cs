using System.Collections.Generic;
using System.Threading.Tasks;
using ClassEnrollmentGrades01.Models;

namespace ClassEnrollmentGrades01.Services
{
    public class DataService : IDataService
    {
        private List<Course> _courses = new();
        private List<Student> _students = new();
        private List<Enrollment> _enrollments = new();

        public DataService()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            _courses = new List<Course>
            {
                new Course { Id = 1, Name = "Математика", Description = "Высшая математика" },
                new Course { Id = 2, Name = "Физика", Description = "Общая физика" },
                new Course { Id = 3, Name = "Программирование", Description = "Основы C#" }
            };

            _students = new List<Student>
            {
                new Student { Id = 1, FirstName = "Иван", LastName = "Петров", Group = "101" },
                new Student { Id = 2, FirstName = "Мария", LastName = "Иванова", Group = "102" },
                new Student { Id = 3, FirstName = "Алексей", LastName = "Сидоров", Group = "101" }
            };

            _enrollments = new List<Enrollment>
            {
                new Enrollment { StudentId = 1, CourseId = 1, Grade = 4.5f, GradeDate = DateTime.Now.AddDays(-5) },
                new Enrollment { StudentId = 1, CourseId = 2, Grade = 4.0f, GradeDate = DateTime.Now.AddDays(-4) },
                new Enrollment { StudentId = 2, CourseId = 1, Grade = 5.0f, GradeDate = DateTime.Now.AddDays(-3) },
                new Enrollment { StudentId = 2, CourseId = 3, Grade = 4.8f, GradeDate = DateTime.Now.AddDays(-2) },
                new Enrollment { StudentId = 3, CourseId = 2, Grade = 3.5f, GradeDate = DateTime.Now.AddDays(-1) },
                new Enrollment { StudentId = 3, CourseId = 3, Grade = 4.2f, GradeDate = DateTime.Now }
            };
        }

        // Синхронные методы
        public List<Course> GetCourses() => _courses;
        public void AddCourse(Course course) => _courses.Add(course);
        public void UpdateCourse(Course course)
        {
            var index = _courses.FindIndex(c => c.Id == course.Id);
            if (index >= 0) _courses[index] = course;
        }
        public void DeleteCourse(int id) => _courses.RemoveAll(c => c.Id == id);

        public List<Student> GetStudents() => _students;
        public void AddStudent(Student student) => _students.Add(student);
        public void UpdateStudent(Student student)
        {
            var index = _students.FindIndex(s => s.Id == student.Id);
            if (index >= 0) _students[index] = student;
        }
        public void DeleteStudent(int id) => _students.RemoveAll(s => s.Id == id);

        public List<Enrollment> GetEnrollments() => _enrollments;
        public void AddEnrollment(Enrollment enrollment) => _enrollments.Add(enrollment);
        public void UpdateEnrollment(Enrollment enrollment)
        {
            var index = _enrollments.FindIndex(e => 
                e.StudentId == enrollment.StudentId && 
                e.CourseId == enrollment.CourseId);
            if (index >= 0) _enrollments[index] = enrollment;
        }
        public void DeleteEnrollment(int studentId, int courseId) => 
            _enrollments.RemoveAll(e => e.StudentId == studentId && e.CourseId == courseId);

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
    }
} 