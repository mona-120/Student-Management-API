using Student_Management_API.Entities;
using Student_Management_API.Dtos;
namespace Student_Management_API.Services
{
    public interface ICourseService
    {
        Task<Course> AddCourse(CreateCourseRequest course);
        Task<Course> UpdateCourse(int id,CreateCourseRequest course);
        Task<Course> GetCourseById(int id);
        Task<List<Course>> GetCourseByTitle(string title);
        Task<List<Course>> GetAllCourses();
        Task<Course> DeleteCourse(int id);
    }
}
