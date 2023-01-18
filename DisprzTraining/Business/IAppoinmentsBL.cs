using DisprzTraining.Models;

namespace DisprzTraining.Business
{
    public interface IAppoinmentBL
    {
        Task<List<Appointment>> GetAppointments(DateTime? startTime, DateTime? endTime, string title);
        // Task<Appointment> GetAppointmentByID(Guid id);
        // Task<List<Appointment>> GetAppointmentByEventName(string eventName);
        Task<Appointment> CreateAppoinment(Appointment data);
        Task<bool> FlagAppoinment(Appointment data);
        Task<Appointment> UpdateStudent(Appointment data);
        Task<Appointment>DeleteStudent(Guid id);
    }
}