namespace ClassEnrollment.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int MaxStudents { get; set; }
        public List<Student> EnrolledStudents { get; set; } = new List<Student>();

        public bool AddStudent(Student student)
        {
            if (EnrolledStudents.Count < MaxStudents)
            {
                EnrolledStudents.Add(student);
                return true;
            }
            return false;
        }

        public bool RemoveStudent(int studentId)
        {
            var student = EnrolledStudents.Find(s => s.Id == studentId);
            if (student != null)
            {
                EnrolledStudents.Remove(student);
                return true;
            }
            return false;
        }
    }
}
