using Student_Management_API.Data;
using Student_Management_API.Dtos;
using Student_Management_API.Entities;
using Microsoft.EntityFrameworkCore;
namespace Student_Management_API.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext db;
        public StudentService(AppDbContext db)
        {
            this.db = db;
        }

        // Add new student
        public async Task<StudentResponse> AddStudent(StudentRequest studentRequest)
        {
            if (await db.Students.AnyAsync(s => s.Email == studentRequest.Email))
                throw new Exception("Email Already Exist!");
            if (studentRequest.DateOfBirth != null && studentRequest.DateOfBirth >= DateOnly.FromDateTime(DateTime.Now))
                throw new Exception("Invalid Date Of birth,It mustn't be in the future!");

            Student student = new Student();
            student.FullName = studentRequest.FullName;
            student.DateOfBirth = studentRequest.DateOfBirth;
            student.Email = studentRequest.Email;

            await db.Students.AddAsync(student);
            await db.SaveChangesAsync();

            StudentResponse response = new StudentResponse();
            response.StudentId = student.StudentId;
            response.FullName = student.FullName;
            response.DateOfBirth = student.DateOfBirth;
            response.Email = student.Email;
            response.EnrollmentDate = student.EnrollmentDate;

            return response;
        }


        // Update Student Info
        public async Task<StudentResponse> UpdateStudentInfo(int id ,StudentRequest studentRequest)
        {
            Student? student = await db.Students.FirstOrDefaultAsync(s=> s.StudentId == id);
            if (student == null)
                throw new Exception("Student Doesn't Exist!");

            if (studentRequest.FullName != student.FullName)
            {
                student.FullName = studentRequest.FullName;
            }
            if(studentRequest.Email != student.Email)
            {
                if (await db.Students.AnyAsync(s => s.Email == studentRequest.Email))
                    throw new Exception("Email Already Exist!");

                student.Email = studentRequest.Email;
            }
            if(studentRequest.DateOfBirth != null && studentRequest.DateOfBirth != student.DateOfBirth)
            {
                if (studentRequest.DateOfBirth >= DateOnly.FromDateTime(DateTime.Now))
                    throw new Exception("Invalid Date Of birth,It mustn't be in the future!");

                student.DateOfBirth = studentRequest.DateOfBirth;
            }

            await db.SaveChangesAsync();

            StudentResponse response = new StudentResponse();
            response.StudentId = student.StudentId;
            response.FullName = student.FullName;
            response.Email = student.Email;
            response.DateOfBirth = student.DateOfBirth;
            response.EnrollmentDate = student.EnrollmentDate;
            return response;
        }


        // Get Student by Id
        public async Task<StudentResponse> GetStudentById(int id)
        {
            Student? student = await db.Students.AsNoTracking().FirstOrDefaultAsync(s => s.StudentId == id);
            if (student == null)
                throw new Exception("Student Doesn't Exist!");

            StudentResponse response = new StudentResponse();
            response.StudentId = student.StudentId;
            response.FullName = student.FullName;
            response.Email = student.Email;
            response.DateOfBirth = student.DateOfBirth;
            response.EnrollmentDate = student.EnrollmentDate;
            return response;
        }


        // Get Student by name (partial match, not an exact match)
        public async Task<List<StudentResponse>> GetStudentByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Please Enter the Name!");

            List<StudentResponse> responses = await db.Students.Where(s => s.FullName.Contains(name)).AsNoTracking()
                .Select(response => new StudentResponse
                {
                    StudentId = response.StudentId,
                    FullName = response.FullName,
                    DateOfBirth = response.DateOfBirth,
                    Email = response.Email,
                    EnrollmentDate = response.EnrollmentDate
                }).ToListAsync();

            if (responses.Count == 0)
                throw new Exception("Student Doesn't Exist");

            return responses;
        }


        // Get All Students
        public async Task<List<StudentResponse>> GetAllStudents()
        {
            List<StudentResponse> responses = await db.Students.AsNoTracking()
                .Select(response => new StudentResponse
                {
                    StudentId = response.StudentId,
                    FullName = response.FullName,
                    Email = response.Email,
                    DateOfBirth = response.DateOfBirth,
                    EnrollmentDate = response.EnrollmentDate
                }).ToListAsync();

            return responses;
        }


        // Delete a student
        public async Task<StudentResponse> DeleteStudent(int id)
        {
            Student? student = await db.Students.FirstOrDefaultAsync(s => s.StudentId == id);
            if (student == null)
                throw new Exception("Student Doesn't Exist!");

            StudentResponse response = new StudentResponse();
            response.StudentId = student.StudentId;
            response.FullName = student.FullName;
            response.Email = student.Email;
            response.DateOfBirth = student.DateOfBirth;
            response.EnrollmentDate = student.EnrollmentDate;

            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return  response;
        }

    }
}
