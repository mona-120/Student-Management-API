using System.ComponentModel.DataAnnotations;

namespace Student_Management_API.Dtos
{
    public class CreateCourseRequest
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Range(2,120)]
        public int Credits { get; set; }
    }
}
