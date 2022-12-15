using DisprzTraining.Models;

namespace DisprzTraining.Business
{
    public interface IStudentBL
    {
        Task<List<Student>> SayHelloWorld();
        Task<Student> GetStudentByID(int id);
        Task<List<Student>> CreateStudent(Student data);
        Task<List<Student>> UpdateStudent(Student data);
        Task<List<Student>> DeleteStudent(int id);
    }
}