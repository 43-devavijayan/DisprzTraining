using System.ComponentModel.DataAnnotations;

namespace DisprzTraining.Models
{
    public class Appointment
    {
        public Guid ID {get; set;}
        [Required]
        public string Name {get; set;} = string.Empty;
        public string meetingUrl {get; set;} = string.Empty;
        public DateTime start {get; set;} 
        public DateTime end {get; set;} 
        public string title { get; set; } = string.Empty;
    }

}
