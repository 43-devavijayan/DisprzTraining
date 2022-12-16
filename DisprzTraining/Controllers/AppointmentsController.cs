using System.Text.Json;
using DisprzTraining.Business;
using DisprzTraining.Models;
using Microsoft.AspNetCore.Mvc;

namespace DisprzTraining.Controllers
{
    [Route("api/appoinment")]
    [ApiController]
    public class AppoinmentController : ControllerBase
    {
        private readonly IAppoinmentBL _appoinmentBL;
        public AppoinmentController(IAppoinmentBL appoinmentBL)
        {
            _appoinmentBL = appoinmentBL;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(Appointment), 200)]
        public async Task<IActionResult> GetAppointmentDetails()
        {
            return Ok(await _appoinmentBL.GetAppointments());
        }

        [HttpGet("ID")]
        [ProducesResponseType(typeof(Appointment), 200)]
        public async Task<IActionResult> GetAppointmentByID(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(" Request cannot be null ");
            }
            try
            {
                var existingAppointment = await _appoinmentBL.GetAppointmentByID(id);
                return Ok( existingAppointment );
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("Event")]
        [ProducesResponseType(typeof(Appointment), 200)]
        public async Task<IActionResult> GetAppointmentByEventName(string eventName)
        {
           if(!(eventName.Equals(string.Empty)))
           {
             try
            {
                var existingAppointment = await _appoinmentBL.GetAppointmentByEventName(eventName);
                return Ok(existingAppointment);
            }
            catch (Exception)
            {
                return NotFound();
            }
           }
           else{
            string result = "Request cannot be null";
            return BadRequest(result);
           }
        }

        [HttpPost("Post")]
        [ProducesResponseType(typeof(Appointment), 201)]
        public async Task<IActionResult> PostAppointmentDetails(Appointment data)
        {
            try
            {
                bool flag = true;
                flag = _appoinmentBL.FlagAppoinment(data).Result;
                if (flag.Equals(false))
                {
                    return Conflict(new { errorMessage = $"ALready Meeting Assigned ", data });
                }

                var meeting = await _appoinmentBL.CreateAppoinment(data);
                return Created("api/appoinments/post", meeting);
            }
            catch (Exception e)
            {
                return BadRequest("Request cannot be null "+e);
            }
        }

       [HttpPut("ID")]
        public async Task<IActionResult> UpdateStudentDetails(Appointment data)
        {
            try
            {
                bool flag = true;
                flag = _appoinmentBL.FlagAppoinment(data).Result;
                if (flag.Equals(false))
                {
                    return Conflict(new { errorMessage = $"Already Meeting Assigned ", data });
                }
                var updateResult = await _appoinmentBL.UpdateStudent(data);
                return Ok(updateResult);
            }
            catch (Exception e)
            {
                return BadRequest("A non-empty request body is required.");
            }
        }

        [HttpDelete("ID")]
        public async Task<IActionResult> DeleteStudentDetails(Guid id)
        {
            if(!(id == Guid.Empty))
            {
                try
            {
                var excistingAppointment = _appoinmentBL.GetAppointmentByID(id);
                await _appoinmentBL.DeleteStudent(id);
                return NoContent();
            }
            catch (Exception e)
            {                
                System.Console.WriteLine(e);
                return NotFound();
            }
            }
            return BadRequest("Request cannot be null");

        }
    }
}
