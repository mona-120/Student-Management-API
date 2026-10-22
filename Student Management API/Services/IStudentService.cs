using Student_Management_API.Dtos;

namespace Student_Management_API.Services
{
    public interface IStudentService
    {
        Task<StudentResponse> AddStudent(StudentRequest studentRequest);
        Task<StudentResponse> UpdateStudentInfo(int id, StudentRequest studentRequest);
        Task<StudentResponse> GetStudentById(int id);
        Task<List<StudentResponse>> GetStudentByName(string name);
        Task<List<StudentResponse>> GetAllStudents();
        Task<StudentResponse> DeleteStudent(int id);
    }
}
