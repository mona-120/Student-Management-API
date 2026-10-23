using Student_Management_API.Dtos;
using Student_Management_API.Entities;

namespace Student_Management_API.Services
{
    public interface IEnrollmentService
    {
        Task<Enrollment> AddEnrollment(CreateEnrollmentRequest enrollmentRequest);
        Task<List<Enrollment>> GetCourseEnrollments(int id);
        Task<List<Enrollment>> GetStudentEnrollments(int id);
        Task<Enrollment> GetEnrollmentByID(int StudentId, int courseId);
        Task<List<Enrollment>> GetAllEnrollments();
        Task<Enrollment> UpdateEnrollment(CreateEnrollmentRequest enrollmentRequest);
        Task<Enrollment> DeleteEnrollment(int StudentId, int CourseId);
    }
}
