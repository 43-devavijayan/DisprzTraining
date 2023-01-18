using System.ComponentModel.DataAnnotations;

namespace DisprzTraining.Models
{
    public class Appointment
    {
        public Guid ID {get; set;}
        [Required]
        public string Name {get; set;} = string.Empty;
        public string meetingUrl {get; set;} = string.Empty;
        public DateTime startTime {get; set;} 
        public DateTime endTime {get; set;} 
        public string title { get; set; } = string.Empty;
    }

}
