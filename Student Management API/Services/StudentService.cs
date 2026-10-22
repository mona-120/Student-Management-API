using Student_Management_API.Common;
using Student_Management_API.Data;
using Student_Management_API.Dtos;
using Student_Management_API.Entities;
using Microsoft.EntityFrameworkCore;
namespace Student_Management_API.Services
{
    public class StudentService
    {
        AppDbContext db;
        public StudentService(AppDbContext db)
        {
            this.db = db;
        }

        // Add new student
        public async Task<ServiceResponse<StudentResponse>> AddStudent(StudentRequest studentRequest)
        {
            if (await db.Students.AnyAsync(s => s.Email == studentRequest.Email))
                return new ServiceResponse<StudentResponse>(false,"Email Already Exist!",null);
            if (studentRequest.DateOfBirth != null && studentRequest.DateOfBirth >= DateOnly.FromDateTime(DateTime.Now))
                return new ServiceResponse<StudentResponse>(false, "Invalid Date Of birth,It mustn't be in the future!", null);

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

            return new ServiceResponse<StudentResponse>(true, $"Student {studentRequest.FullName} Added Successfully!", response);
        }


        // Update Student Info
        public async Task<ServiceResponse<StudentResponse>> UpdateStudentInfo(int id ,StudentRequest studentRequest)
        {
            Student? student = await db.Students.FirstOrDefaultAsync(s=> s.StudentId == id);
            if (student == null)
                return new ServiceResponse<StudentResponse>(false, "Student Doesn't Exist!", null);

            if(studentRequest.FullName != student.FullName)
            {
                student.FullName = studentRequest.FullName;
            }
            if(studentRequest.Email != student.Email)
            {
                if (await db.Students.AnyAsync(s => s.Email == studentRequest.Email))
                    return new ServiceResponse<StudentResponse>(false, "Email Already Exist!", null);

                student.Email = studentRequest.Email;
            }
            if(studentRequest.DateOfBirth != null && studentRequest.DateOfBirth != student.DateOfBirth)
            {
                if (studentRequest.DateOfBirth >= DateOnly.FromDateTime(DateTime.Now))
                    return new ServiceResponse<StudentResponse>(false, "Invalid Date Of birth,It mustn't be in the future!", null);

                student.DateOfBirth = studentRequest.DateOfBirth;
            }

            await db.SaveChangesAsync();

            StudentResponse response = new StudentResponse();
            response.StudentId = student.StudentId;
            response.FullName = student.FullName;
            response.Email = student.Email;
            response.DateOfBirth = student.DateOfBirth;
            response.EnrollmentDate = student.EnrollmentDate;
            return new ServiceResponse<StudentResponse>(true, $"Student {studentRequest.FullName} Updated Successfully!", response);
        }


        // Get Student by Id
        public async Task<ServiceResponse<StudentResponse>> GetStudentById(int id)
        {
            Student? student = await db.Students.AsNoTracking().FirstOrDefaultAsync(s => s.StudentId == id);
            if (student == null)
                return new ServiceResponse<StudentResponse>(false, "Student Doesn't Exist!", null);

            StudentResponse response = new StudentResponse();
            response.StudentId = student.StudentId;
            response.FullName = student.FullName;
            response.Email = student.Email;
            response.DateOfBirth = student.DateOfBirth;
            response.EnrollmentDate = student.EnrollmentDate;
            return new ServiceResponse<StudentResponse>(true, $"Student {response.FullName} Found!", response);
        }


        // Get Student by name (partial match, not an exact match)
        public async Task<ServiceResponse<List<StudentResponse>>> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new ServiceResponse<List<StudentResponse>>(false, "Please Enter the Name!", null);

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
                return new ServiceResponse<List<StudentResponse>>(false, "Student Doesn't Exist!", null);

            return new ServiceResponse<List<StudentResponse>>(true, $"Found a Students with matched name {name}", responses);
        }


        // Get All Students
        public async Task<ServiceResponse<List<StudentResponse>>> GetAllStudents()
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

            if (responses.Count == 0)
                return new ServiceResponse<List<StudentResponse>>(false, "System Doesn't Contain Students", null);

            return new ServiceResponse<List<StudentResponse>>(true, "Student List Retrived Successfully", responses);
        }


        // Delete a student
        public async Task<ServiceResponse<StudentResponse>> DeleteStudent(int id)
        {
            Student? student = await db.Students.FirstOrDefaultAsync(s => s.StudentId == id);
            if (student == null)
                return new ServiceResponse<StudentResponse>(false, "Student Doesn't Exist!", null);

            StudentResponse response = new StudentResponse();
            response.StudentId = student.StudentId;
            response.FullName = student.FullName;
            response.Email = student.Email;
            response.DateOfBirth = student.DateOfBirth;
            response.EnrollmentDate = student.EnrollmentDate;

            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return new ServiceResponse<StudentResponse>(true, $"Student {student.FullName} Deleted Successfully!", response);
        }

    }
}
