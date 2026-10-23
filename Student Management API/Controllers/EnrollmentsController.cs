using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management_API.Dtos;
using Student_Management_API.Entities;
using Student_Management_API.Services;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments()
        {
                var enrollments = await _enrollmentService.GetAllEnrollments();
                return Ok(enrollments);
        }

        [HttpGet("course/{courseId:int}")]
        public async Task<IActionResult> GetCourseEnrollments(int courseId)
        {
            try
            {
                var enrollments = await _enrollmentService.GetCourseEnrollments(courseId);
                return Ok(enrollments);
            }
            catch(Exception ex)
            {
                if (ex.Message == "Course Doesn't Exist!")
                    return NotFound(ex.Message);
            }
        }

        [HttpGet("student/{StudentId:int}")]
        public async Task<IActionResult> GetStudentEnrollments(int StudentId)
        {
            try
            {
                var enrollments = await _enrollmentService.GetStudentEnrollments(StudentId);
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Student Doesn't Exist!")
                    return NotFound(ex.Message);
            }
        }

        [HttpGet("{studentId:int}/{courseId:int}")]
        public async Task<IActionResult> GetEnrollmentByID(int studentId,int courseId)
        {
            try
            {
                var enrollment = await _enrollmentService.GetEnrollmentByID(studentId,courseId);
                return Ok(enrollment);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEnrollment(CreateEnrollmentRequest request)
        {
            try
            {
                Enrollment enrollmnet = await _enrollmentService.AddEnrollment(request);
                return Created();
            }
            catch(Exception ex)
            {
                if (ex.Message == "Course Doesn't Exist!" || ex.Message == "Student Doesn't Exist!")
                    return NotFound(ex.Message);
                if (ex.Message == "Enrollment Already Exist!")
                    return Conflict(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateGrade(CreateEnrollmentRequest request)
        {
            try
            {
                Enrollment enrollmnet = await _enrollmentService.UpdateEnrollment(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Enrollment Doesn't Exist")
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{studentId:int}/{courseId:int}")]
        public async Task<IActionResult> DeleteEnrollment(int studentId, int courseId)
        {
            try
            {
                await _enrollmentService.DeleteEnrollment(studentId,courseId);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Enrollment Doesn't Exist")
                    return NotFound(ex.Message);
            }
        }
    }
}
