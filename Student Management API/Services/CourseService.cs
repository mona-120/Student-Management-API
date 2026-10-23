using Microsoft.EntityFrameworkCore;
using Student_Management_API.Data;
using Student_Management_API.Dtos;
using Student_Management_API.Entities;

namespace Student_Management_API.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _appDbContext;
        public CourseService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        // Add new Course
        public async Task<Course> AddCourse(CreateCourseRequest courseRequest)
        {
            if (await _appDbContext.Courses.AnyAsync(c => c.Title == courseRequest.Title))
                throw new Exception("Course Already Exist!");

            Course course = new Course();
            course.Title = courseRequest.Title;
            course.Description = courseRequest.Description;
            course.Credits = courseRequest.Credits;

            await _appDbContext.Courses.AddAsync(course);
            await _appDbContext.SaveChangesAsync();
            return course;
        }


        // Delete Course
        public async Task<Course> DeleteCourse(int id)
        {
            Course? course = await _appDbContext.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
            if (course == null)
                throw new Exception("Course not Exist!");

            // Prevent Delete course with students as its delete type is Restrict
            if (await _appDbContext.Enrollments.AnyAsync(e => e.CourseId == id))
                throw new Exception($"Can't Delete course {course.Title}, It Contains Students!");

            _appDbContext.Courses.Remove(course);
            await _appDbContext.SaveChangesAsync();
            return course;
        }


        // Get All Courses
        public async Task<List<Course>> GetAllCourses()
        {
            List<Course> courses = await _appDbContext.Courses.AsNoTracking().ToListAsync();
            return courses;
        }


        //Get Course By Id
        public async Task<Course> GetCourseById(int id)
        {
            Course? course = await _appDbContext.Courses.AsNoTracking().FirstOrDefaultAsync(c=> c.CourseId == id);
            if (course == null)
                throw new Exception("Course not Exist!");

            return course;
        }


        // Get Course By Title
        public async Task<List<Course>> GetCourseByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("Please Enter the Title!");
            List<Course> courses = await _appDbContext.Courses.Where(c=> c.Title.Contains(title)).AsNoTracking().ToListAsync();

            if (courses.Count == 0)
                throw new Exception("Course Doesn't Exist");
            return courses;
        }


        // Update Course
        public async Task<Course> UpdateCourse(int id,CreateCourseRequest courseRequest)
        {
            Course? course = await _appDbContext.Courses.FirstOrDefaultAsync(c => c.CourseId == id);

            if (course == null)
                throw new Exception("Course Doesn't Exist!");
            if(course.Title != courseRequest.Title)
            {
                if (await _appDbContext.Courses.AnyAsync(c => c.Title == courseRequest.Title))
                    throw new Exception("Course Already Exist!");
                course.Title = courseRequest.Title;
            }
            if(course.Description  != courseRequest.Description)
            {
                course.Description = courseRequest.Description;
            }
            if(course.Credits != courseRequest.Credits)
            {
                course.Credits = courseRequest.Credits;
            }

            await _appDbContext.SaveChangesAsync();
            return course;
        }
    }
}
