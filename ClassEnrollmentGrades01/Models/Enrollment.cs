using System.ComponentModel.DataAnnotations.Schema;

namespace ClassEnrollmentGrades01.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public float Grade { get; set; }
        public DateTime GradeDate { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; } = null!;
        
        [ForeignKey("CourseId")]
        public Course Course { get; set; } = null!;
    }
} 