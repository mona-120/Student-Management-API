using Student_Management_API.Entities;

namespace Student_Management_API.Dtos
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int? Grade { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
