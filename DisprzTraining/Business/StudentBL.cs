using DisprzTraining.DataAccess;
using DisprzTraining.Models;

namespace DisprzTraining.Business
{
    public class StudentBL : IStudentBL
    {
        private readonly IStudentDAL _helloWorldDAL;
        public StudentBL(IStudentDAL helloWorldDAL)
        {
            _helloWorldDAL = helloWorldDAL;
        }

        public async Task<List<Student>> SayHelloWorld()
        {
            return await _helloWorldDAL.GetHelloWorldMessage();
        }

         public async Task<Student> GetStudentByID(int id)
        {
            return await _helloWorldDAL.GetStudent(id);
        }

        public async Task<List<Student>> CreateStudent(Student data)
        {
            return await _helloWorldDAL.PostStudentData(data);
        }

        public async Task<List<Student>> UpdateStudent(Student data)
        {
            return await _helloWorldDAL.UpdateStudentData(data);
        }

          public async Task<List<Student>> DeleteStudent(int id)
        {
            return await _helloWorldDAL.DeleteStudentData(id);
        }
    }
}
