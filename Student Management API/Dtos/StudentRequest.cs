using System.ComponentModel.DataAnnotations;

namespace Student_Management_API.Dtos
{
    public class StudentRequest
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        // Ignore StudentId to avoid overposting problem and EnrollmentDate as EF generate it automatically
    }
}
