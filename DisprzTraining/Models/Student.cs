namespace DisprzTraining.Models
{
    public class Student
    {
         public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int departmentId { get; set; }
        public string departmentName { get; set; } = string.Empty;
        
    }
}
