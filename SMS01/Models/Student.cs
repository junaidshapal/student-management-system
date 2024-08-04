using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS01.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        //[Range(1, int.MaxValue, ErrorMessage = "Please enter age bigger than {1}")]
        [Required]
        public int Age { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Father Name")]
        [Required]
        public string FatherName { get; set; } = string.Empty;
        [DisplayName("Reg No")]
        [Required]
        //[Range(1, int.MaxValue, ErrorMessage = "Registration number should start from {1}")]
        public int RegNo { get; set; }
        [Required]
        public string Department { get; set; } = string.Empty;
        [Required]
        public string Semester { get; set; } = string.Empty;
        [Required]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^[0-9]{5}-[0-9]{7}-[0-9]$", ErrorMessage = "CNIC No must follow the XXXXX-XXXXXXX-X format!")]
        public string CNIC { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; }= string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile Image { get; set; }
    }
}
