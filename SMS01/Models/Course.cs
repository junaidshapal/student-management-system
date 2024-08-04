using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS01.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CreditHours { get; set; } = 0;
    }

    public class LecturerCourse
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        [ForeignKey("Lecturer")]
        public int LecturerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
