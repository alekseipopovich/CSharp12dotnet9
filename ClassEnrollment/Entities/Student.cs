namespace ClassEnrollment.Entities
{
    public class Student : Person
    {

        public static int counter;

        public Student() { }

        public Student(string name) : this(Student.counter, name, "testgroup", 20, new()) { }

        public Student(string fname, string lname, string group) : this(Student.counter, fname, group, 20, new()) { }

        public Student(int id, string name, string group, int age, Course course) : base(name,name,age,"")
        {
            Id = id;
            GroupStudents = group;
            Age = age;
            Course = course;
            Student.counter++;
            Console.WriteLine($"counter: {counter}");
        } 

        public int Id { get; set; }
        
        public string GroupStudents { get; set; }

        public int Age { get; set; }
        public Course Course { get; set; }
    }
}
