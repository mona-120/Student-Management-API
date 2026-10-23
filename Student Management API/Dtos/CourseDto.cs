namespace Student_Management_API.Dtos
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int? Grade { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
