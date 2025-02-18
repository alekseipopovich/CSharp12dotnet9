namespace ClassEnrollment.Entities
{
    public class Student : Person
    {

        public static int counter;

        public Student() { }

        public Student(string name) : this(Student.counter, name, 20, new()) { }
        public Student(int id, string name, int age, Course course)
        {
            Id = id;
            Name = name;
            Age = age;
            Course = course;
            Student.counter++;
            Console.WriteLine($"counter: {counter}");
        } 

        public int Id { get; set; }
        
        public string Name { get; set; }

        public int Age { get; set; }
        public Course Course { get; set; }
    }
}
