using System.ComponentModel.DataAnnotations;

namespace ClassEnrollmentGrades01.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        
        [Required]
        [MaxLength(500)]
        public required string Description { get; set; }
    }
} 