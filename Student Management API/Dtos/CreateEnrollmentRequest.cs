using System.ComponentModel.DataAnnotations;

namespace Student_Management_API.Dtos
{
    public class CreateEnrollmentRequest
    {
        [Range(1,int.MaxValue)]
        public int StudentId { get; set; }
        [Range(1,int.MaxValue)]
        public int CourseId { get; set; }
        [Range(0,100)]
        public int? Grade { get; set; }
    }
}
