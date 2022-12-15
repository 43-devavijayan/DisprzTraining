using DisprzTraining.Business;
using DisprzTraining.Models;
using Microsoft.AspNetCore.Mvc;

namespace DisprzTraining.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentBL _studentBL;
        public StudentController(IStudentBL studentBL)
        {
            _studentBL= studentBL;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Appointment), 200)]
        public async Task<IActionResult> Helloworld()
        {
            return Ok(await _studentBL.SayHelloWorld());
        }

        [HttpGet("student")]
        [ProducesResponseType(typeof(Appointment), 200)]
        public async Task<IActionResult> GetStudentByID(int id)
        {
            return Ok(await _studentBL.GetStudentByID(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostStudentDetails(Student data)
        {
            return Ok(await _studentBL.CreateStudent(data));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudentDetails(Student data)
        {
            return Ok(await _studentBL.UpdateStudent(data));
        }

        //  [HttpDelete("ID")]
        // public async Task<IActionResult> DeleteStudentDetails(Guid id)
        // {
        //     return Ok(await _studentBL.DeleteStudent(id));
        // }
    }
}
