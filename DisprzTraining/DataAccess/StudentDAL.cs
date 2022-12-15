using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{
    public class StudentDAL : IStudentDAL
    {
       public static List<Student> studentData = new(){

            new Student { ID=1, Name = "Deva", Age = 21, departmentId = 231, departmentName = "IT"},
            new Student { ID=2, Name = "Anny", Age = 20, departmentId = 221, departmentName = "CSE"},
            new Student { ID=3, Name = "Navai", Age = 21, departmentId = 231, departmentName = "IT"},
            new Student { ID=4, Name = "Sam", Age = 19, departmentId = 220, departmentName = "CIVIL"}
        };
        public async Task<List<Student>> GetHelloWorldMessage()
        {
            return await Task.FromResult(studentData);
        }

        public async Task<Student> GetStudent(int id)
        {
            foreach (var student in studentData)
            {
                if (student.ID == id)
                {
                    return await Task.FromResult(student);
                }
            }
            return null;
        }

        public async Task<List<Student>> PostStudentData(Student data)
        {
            studentData.Add(data);
            return await Task.FromResult(studentData);
        }

        public async Task<List<Student>> UpdateStudentData(Student data)
        {
            var studentDetail = studentData.Where(student => student.ID == data.ID).First();
            System.Console.WriteLine(studentDetail.Name);
            studentDetail .ID= data.ID;
            studentDetail.Name = data.Name;
            studentDetail.Age = data.Age;
            studentDetail.departmentId = data.departmentId;
            studentDetail.departmentName = data.departmentName;
            return await Task.FromResult(studentData);
        }

         public async Task<List<Student>> DeleteStudentData(int id)
        {
            var studentDetail = studentData.Where(student => student.ID == id).First();
            studentData.Remove(studentDetail);
            return await Task.FromResult(studentData);
        }
    }
}
