namespace Student_Management_API.Dtos
{
    public class StudentResponse
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string Email { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
