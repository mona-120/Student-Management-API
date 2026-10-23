using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management_API.Services;
using Student_Management_API.Dtos;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
                var students = await _studentService.GetAllStudents();
                return Ok(students);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var student = await _studentService.GetStudentById(id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetStudentByName([FromQuery]string name)
        {
            try
            {
                var student = await _studentService.GetStudentByName(name);
                return Ok(student);
            }
            catch (Exception ex)
            {
                    return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentRequest student)
        {
            try
            {
                StudentResponse response =  await _studentService.AddStudent(student);
                return Created();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Email Already Exist!")
                    return Conflict(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStudent(int id,StudentRequest student)
        {
            try
            {
                await _studentService.UpdateStudentInfo(id,student);
                return NoContent();
            }
            catch(Exception ex)
            {
                if (ex.Message == "Student Doesn't Exist!")
                    return NotFound(ex.Message);
                if (ex.Message == "Email Already Exist!")
                    return Conflict(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteStudent(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("{id:int}/courses")]
        public async Task<IActionResult> GetStudentDetails(int id)
        {
            try
            {
                var details = await _studentService.StudentInfo(id);
                return Ok(details);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
