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

        public async Task<List<Appointment>> GetAppointments()
        {
            return await _appoinmentDAL.GetAllAppointments();
        }

        public async Task<Appointment> GetAppointmentByID(Guid id)
        {
            var resultByID = await _appoinmentDAL.GetDataByID(id);
            if (resultByID == null)
            {
                throw new NullReferenceException();
            }
            return (resultByID);
        }

        public async Task<List<Appointment>> GetAppointmentByEventName(string eventName)
        {
            var resultByName = await _appoinmentDAL.GetDataByEventName(eventName);
            if (resultByName.Count <= 0)
            {
                throw new NullReferenceException();
            }
            return (resultByName);
        }

        public async Task<bool> FlagAppoinment(Appointment data)
        {
            var meetingDetails = await _appoinmentDAL.GetAllAppointments();
            // Task<List<Appointment>> meetingDetails =  _helloWorldDAL.GetAllAppointments();
            Boolean flag = true;
            foreach (var details in meetingDetails)
            {
                if ((data.startTime >= details.startTime) && (data.startTime <= details.endTime))
                {
                    flag = false;
                }
                else if ((data.endTime >= details.startTime) && (data.endTime <= details.endTime))
                {
                    flag = false;
                }
                else if ((data.startTime <= details.startTime) && (data.endTime >= details.endTime))
                {
                    flag = false;
                }
            }
            return flag;
        }
        public async Task<Appointment> CreateAppoinment(Appointment Data)
        {
            var meetingDetails = await _appoinmentDAL.Postappointment(Data);
            if (meetingDetails == null)
            {
                throw new NullReferenceException();
            }
            return meetingDetails;
        }

        public async Task<Appointment> UpdateStudent(Appointment data)
        {
            var updatedResult = await _appoinmentDAL.UpdateAppoinmentDetail(data);
            if (updatedResult == null)
            {
                throw new NullReferenceException();
            }
            return updatedResult;
        }

        public async Task<Appointment> DeleteStudent(Guid id)
        {
            var deletedResult = await _appoinmentDAL.DeleteStudentData(id);
            if (deletedResult == null)
            {
                throw new NullReferenceException();
            }
            return deletedResult;
        }
    }
}
