using Microsoft.EntityFrameworkCore;
using Student_Management_API.Data;
using Student_Management_API.Dtos;
using Student_Management_API.Entities;

namespace Student_Management_API.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext _appDbContext;
        public EnrollmentService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        //Add New enrollment
        public async Task<Enrollment> AddEnrollment(CreateEnrollmentRequest enrollmentRequest)
        {
            bool studentExist = await _appDbContext.Students.AnyAsync(s => s.StudentId == enrollmentRequest.StudentId);
            if (!studentExist)
                throw new Exception("Student Doesn't Exist!");

            bool courseExist = await _appDbContext.Courses.AnyAsync(c=> c.CourseId == enrollmentRequest.CourseId);
            if (!courseExist)
                throw new Exception("Course Doesn't Exist!");

            bool enrollmentExist = await _appDbContext.Enrollments.AnyAsync(e => e.CourseId == enrollmentRequest.CourseId
                                         && e.StudentId == enrollmentRequest.StudentId);
            if (enrollmentExist)
                throw new Exception("Enrollment Already Exist!");

            Enrollment enrollment = new Enrollment();
            enrollment.StudentId = enrollmentRequest.StudentId;
            enrollment.CourseId = enrollmentRequest.CourseId;
            enrollment.Grade = enrollmentRequest.Grade;

            await _appDbContext.Enrollments.AddAsync(enrollment);
            await _appDbContext.SaveChangesAsync();
            return enrollment;
        }


        // Delete Enrollment
        public async Task<Enrollment> DeleteEnrollment(int StudentId,int CourseId)
        {
            Enrollment? enrollment = await _appDbContext.Enrollments.FirstOrDefaultAsync(e=> e.StudentId == StudentId
                                        &&  e.CourseId == CourseId);
            if (enrollment == null)
                throw new Exception("Enrollment Doesn't Exist");

            _appDbContext.Enrollments.Remove(enrollment);
            await _appDbContext.SaveChangesAsync();
            return enrollment;
        }


        // Get All Enrollments
        public async Task<List<Enrollment>> GetAllEnrollments()
        {
            var enrollments = await _appDbContext.Enrollments.Include(e=> e.Student)
                                          .Include(e=>e.Course).AsNoTracking().ToListAsync();
    
            return enrollments;
        }


        // Get Course Enrollments
        public async Task<List<Enrollment>> GetCourseEnrollments(int courseId)
        {
            bool courseExist = await _appDbContext.Courses.AnyAsync(c => c.CourseId == courseId);
            if (!courseExist)
                throw new Exception("Course Doesn't Exist!");

            List<Enrollment> enrollments = await _appDbContext.Enrollments.Where(e=> e.CourseId == courseId)
                                            .Include(e=> e.Student).Include(e=> e.Course).AsNoTracking().ToListAsync();

            return enrollments;
        }

        public async Task<Enrollment> GetEnrollmentByID(int studentId, int courseId)
        {
            bool studentExist = await _appDbContext.Students.AnyAsync(s => s.StudentId == studentId);
            if (!studentExist)
                throw new Exception("Student Doesn't Exist!");
            bool courseExist = await _appDbContext.Courses.AnyAsync(c => c.CourseId == courseId);
            if (!courseExist)
                throw new Exception("Course Doesn't Exist!");

            Enrollment? enrollment = await _appDbContext.Enrollments.AsNoTracking()
                                        .Include(e => e.Student).Include(e => e.Course)
                                        .FirstOrDefaultAsync(e=> e.StudentId == studentId && e.CourseId == courseId);
            if (enrollment == null)
                throw new Exception("Enrollment Doesn't Exist");
            return enrollment;
        }

        public async Task<List<Enrollment>> GetStudentEnrollments(int studentId)
        {
            bool studentExist = await _appDbContext.Students.AnyAsync(s => s.StudentId == studentId);
            if (!studentExist)
                throw new Exception("Student Doesn't Exist!");

            List<Enrollment> enrollments = await _appDbContext.Enrollments.Where(e => e.StudentId == studentId)
                                                     .Include(e => e.Student).Include(e => e.Course)
                                                     .AsNoTracking().ToListAsync();

            return enrollments;
        }

        public async Task<Enrollment> UpdateEnrollment(CreateEnrollmentRequest enrollmentRequest)
        {
            Enrollment? enrollment = await _appDbContext.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == enrollmentRequest.StudentId && e.CourseId == enrollmentRequest.CourseId);
            if (enrollment == null)
                throw new Exception("Enrollment Doesn't Exist");

            enrollment.Grade = enrollmentRequest.Grade;
            await _appDbContext.SaveChangesAsync();
            return enrollment;
        }
    }
}
