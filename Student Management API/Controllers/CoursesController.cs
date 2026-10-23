using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management_API.Dtos;
using Student_Management_API.Services;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var courses = await _courseService.GetAllCourses();
                return Ok(courses);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            try
            {
                var course = await _courseService.GetCourseById(id);
                return Ok(course);
            }
            catch (Exception ex)
            {
               if(ex.Message == "Course not Exist!")
                    return NotFound(ex.Message);
               return BadRequest(ex.Message);
            }
        }

        [HttpGet("{title:alpha}")]
        public async Task<IActionResult> GetCourseByTitle(string title)
        {
            try
            {
                var course = await _courseService.GetCourseByTitle(title);
                return Ok(course);
            }
            catch(Exception ex)
            {
                if (ex.Message == "Course Doesn't Exist")
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CreateCourseRequest request)
        {
            try
            {
                var course = await _courseService.AddCourse(request);
                return Ok(course);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCourse(int id, CreateCourseRequest request)
        {
            try
            {
                var course = await _courseService.UpdateCourse(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Course Doesn't Exist!")
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                await _courseService.DeleteCourse(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                if (ex.Message == "Course Doesn't Exist!")
                    return NotFound(ex.Message);
                
                return BadRequest(ex.Message);
            }
        }
    }
}
