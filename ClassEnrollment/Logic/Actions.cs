using ClassEnrollment.Entities;

namespace ClassEnrolment.Logic
{
    public static class Actions
    {
        public static List<Course> courses = new List<Course>();

        public static void AddCourse()
        {
            Console.Write("Введите ID курса: ");
            int courseId = int.Parse(Console.ReadLine());
            Console.Write("Введите название курса: ");
            string courseName = Console.ReadLine();
            Console.Write("Введите максимальное количество студентов: ");
            int maxStudents = int.Parse(Console.ReadLine());

            courses.Add(new Course { CourseId = courseId, CourseName = courseName, MaxStudents = maxStudents });
        }

        public static void EnrollStudent()
        {
            Console.Write("Введите ID студента: ");
            int studentId = int.Parse(Console.ReadLine());
            Console.Write("Введите имя студента: ");
            string studentName = Console.ReadLine();
            Console.Write("Введите возраст студента: ");
            int studentAge = int.Parse(Console.ReadLine());

            Console.Write("Введите ID курса для записи: ");
            int courseId = int.Parse(Console.ReadLine());

            var course = courses.Find(c => c.CourseId == courseId);
            if (course != null)
            {
                if (course.AddStudent(new Student { Id = studentId, Name = studentName, Age = studentAge }))
                {
                    Console.WriteLine("Студент успешно записан на курс.");
                }
                else
                {
                    Console.WriteLine("Курс переполнен.");
                }
            }
            else
            {
                Console.WriteLine("Курс не найден.");
            }
        }

        public static void ShowStudents()
        {
            Console.Write("Введите ID курса: ");
            int courseId = int.Parse(Console.ReadLine());

            var course = courses.Find(c => c.CourseId == courseId);
            if (course != null)
            {
                foreach (var student in course.EnrolledStudents)
                {
                    Console.WriteLine($"ID: {student.Id}, Имя: {student.Name}, Возраст: {student.Age}");
                }
            }
            else
            {
                Console.WriteLine("Курс не найден.");
            }
        }

        public static void RemoveStudent()
        {
            try
            {
                Console.Write("Введите ID курса: ");
                int courseId = int.Parse(Console.ReadLine());

                var course = courses.Find(c => c.CourseId == courseId);
                if (course == null)
                {
                    throw new Exception("Курс с указанным ID не найден.");
                }

                Console.Write("Введите ID студента: ");
                int studentId = int.Parse(Console.ReadLine());

                if (course.RemoveStudent(studentId))
                {
                    Console.WriteLine("Студент успешно удален из курса.");
                }
                else
                {
                    Console.WriteLine("Студент не найден на курсе.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public static void ShowCourses()
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("Список курсов пуст.");
                return;
            }

            Console.WriteLine("Список курсов:");
            foreach (var course in courses)
            {
                Console.WriteLine($"ID: {course.CourseId}, Название: {course.CourseName}, Максимум студентов: {course.MaxStudents}");
            }
        }

        public static void RemoveCourse()
        {
            Console.Write("Введите ID курса для удаления: ");
            int courseId = int.Parse(Console.ReadLine());

            var course = courses.Find(c => c.CourseId == courseId);
            if (course != null)
            {
                courses.Remove(course);
                Console.WriteLine("Курс успешно удален.");
            }
            else
            {
                Console.WriteLine("Курс не найден.");
            }
        }

        public static void ShowAllStudents()
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("Список курсов пуст.");
                return;
            }

            Console.WriteLine("Список всех студентов на всех курсах:");
            foreach (var course in courses)
            {
                Console.WriteLine($"Курс: {course.CourseName} (ID: {course.CourseId})");
                foreach (var student in course.EnrolledStudents)
                {
                    Console.WriteLine($"  ID: {student.Id}, Имя: {student.Name}, Возраст: {student.Age}");
                }
            }
        }
    }
}