using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{
    public interface IAppoinmentDAL
    {
        Task<List<Appointment>> GetAllAppointments();
        // Task<Appointment> GetDataByID(Guid id);
        // Task<List<Appointment>> GetDataByEventName(string Name);
        Task<Appointment> Postappoinment(Appointment Data);
        Task<Appointment> UpdateAppoinmentDetail(Appointment Data);
        Task<Appointment> DeleteStudentData(Guid id);
    }
}
