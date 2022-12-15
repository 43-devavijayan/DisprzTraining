using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{
    public interface IStudentDAL
    {
        Task<List<Student>> GetHelloWorldMessage();
        Task<Student> GetStudent(int id);
        Task<List<Student>> PostStudentData(Student Data);
        Task<List<Student>> UpdateStudentData(Student Data);
        Task<List<Student>> DeleteStudentData(int id);
    }
}
