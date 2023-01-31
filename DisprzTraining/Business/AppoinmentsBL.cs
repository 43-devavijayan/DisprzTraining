using System.Text.Json;
using DisprzTraining.DataAccess;
using DisprzTraining.Models;

namespace DisprzTraining.Business
{
    public class AppointmentBL : IAppoinmentBL
    {
        private readonly IAppoinmentDAL _appoinmentDAL;
        public AppointmentBL(IAppoinmentDAL appoinmentDAL)
        {
            _appoinmentDAL = appoinmentDAL;
        }

        public async Task<List<Appointment>> GetAppointments(DateTime? startTime, DateTime? endTime, string title)
        {
            var resultappoinment = await _appoinmentDAL.GetAllAppointments();
            // var query =new List<Appointment>();
            IEnumerable<Appointment> query = resultappoinment;
            if ((!(startTime == null)) && (!(endTime == null) && (!string.IsNullOrEmpty(title))))
            {
                query = resultappoinment.Where(e => e.start == startTime && e.end== endTime && e.title.Contains(title));
            }
            if (((startTime == null)) && ((endTime == null)) && (!string.IsNullOrEmpty(title)))
            {
                query = resultappoinment.Where(e => e.title.Contains(title));
            }
            if ((!(startTime == null)) && (string.IsNullOrEmpty(title)) && (endTime == null))
            {
                query = resultappoinment.Where(e => e.start == startTime);
            }
            if ((!(endTime == null)) && ((startTime == null)) && (string.IsNullOrEmpty(title)))
            {
                query = resultappoinment.Where(e => e.end == endTime);
            }
             if ((!(endTime == null) ) && (!(startTime == null)) && (string.IsNullOrEmpty(title)))
            {
                query = resultappoinment.Where(e => e.end == endTime);
            }
             if ((!(endTime == null)) && (!(string.IsNullOrEmpty(title))) && (startTime == null))
            {
                query = resultappoinment.Where(e => e.end == endTime);
            }
            if ((!(startTime == null)) && (!(string.IsNullOrEmpty(title))) && (endTime == null))
            {
                query = resultappoinment.Where(e => e.start == startTime);
            }
            
            var result = new List<Appointment>(query);
            
            if (result == null)
            {
                throw new NullReferenceException();
            }
            return result;
        }

        // public async Task<Appointment> GetAppointmentByID(Guid id)
        // {
        //     var resultByID = await _appoinmentDAL.GetDataByID(id);
        //     if (resultByID == null)
        //     {
        //         throw new NullReferenceException();
        //     }
        //     return (resultByID);
        // }

        // public async Task<List<Appointment>> GetAppointmentByEventName(string eventName)
        // {
        //     var resultByName = await _appoinmentDAL.GetDataByEventName(eventName);
        //     if (resultByName.Count <= 0)
        //     {
        //         throw new NullReferenceException();
        //     }
        //     return (resultByName);
        // }

        public async Task<bool> FlagAppoinment(Appointment data)
        {
            var meetingDetails = await _appoinmentDAL.GetAllAppointments();
            Boolean flag = true;
            foreach (var details in meetingDetails)
            {
                // System.Console.WriteLine(details.start);
                if (data.ID != details.ID)
                {
                    if ((data.start >= details.start) && (data.start <= details.end))
                    {
                        flag = false;
                    }
                    else if ((data.end >= details.start) && (data.end <= details.end))
                    {
                        flag = false;
                    }
                    else if ((data.start <= details.start) && (data.end >= details.end))
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }
        public async Task<Appointment> CreateAppoinment(Appointment data)
        {
            if (data == null)
            {
                throw new NullReferenceException();
            }
            var meetingDetails = await _appoinmentDAL.Postappoinment(data);
            return meetingDetails;
        }

        public async Task<Appointment> UpdateStudent(Appointment data)
        {
            if (data == null)
            {
                throw new NullReferenceException();
            }
            var updatedResult = await _appoinmentDAL.UpdateAppoinmentDetail(data);
            return updatedResult;
        }

        public async Task<Appointment> DeleteStudent(Guid id)
        {
            var deletedResult = await _appoinmentDAL.DeleteStudentData(id);
            if (deletedResult == null)
            {
                throw new InvalidOperationException();
            }
            return deletedResult;
        }
    }
}
