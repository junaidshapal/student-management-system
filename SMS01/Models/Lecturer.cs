using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SMS01.Models
{
    public class Lecturer
    {
        public int LecturerId { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public int Age { get; set; }
        [Required]
        public string Department { get; set; } = string.Empty;
        [Required]
        public string CNIC { get; set; } = string.Empty;

        public int RoleId { get; set; }
    }
}
