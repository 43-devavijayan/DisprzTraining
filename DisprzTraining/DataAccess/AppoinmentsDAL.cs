using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{
    public class AppoinmentDAL : IAppoinmentDAL
    {
        private static List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        private static List<char> characters = new List<char>()
         {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
        'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B',
        'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
        'Q', 'R', 'S',  'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_'};

        public static string GetURL()
        {
            string URL = "https://api/appoinments/";
            Random rand = new Random();
            for (int i = 0; i < 15; i++)
            {
                int random = rand.Next(0, 3);
                if (random == 1)
                {
                    random = rand.Next(0, numbers.Count);
                    URL += numbers[random].ToString();
                }
                else
                {
                    random = rand.Next(0, characters.Count);
                    URL += characters[random].ToString();
                }
            }
            return URL;
        }

        public static List<Appointment> appointmentDetails = new(){
            
            new Appointment{ ID = new Guid("d780857c-2df6-4b12-b484-97b75db63215"), Name = "Devasangeetha", meetingUrl = GetURL(), startTime = new DateTime(2022, 12, 31, 5, 10, 20, DateTimeKind.Utc), endTime = new DateTime(2022, 12, 31, 5, 20, 00, DateTimeKind.Utc), eventName ="Scrumcall"},
            new Appointment{ ID = new Guid("766fdce0-7e9c-4c43-b068-02fd99c008d5"), Name = "NiravDoshi", meetingUrl = GetURL(), startTime = new DateTime(2022, 12, 31, 1, 30, 00, DateTimeKind.Utc), endTime = new DateTime(2022, 12, 31, 2, 00, 00, DateTimeKind.Utc), eventName ="Scrumcall"},
            new Appointment{ ID = new Guid("ed46a787-9522-47cb-897a-96714b4c877e"), Name = "VijayShree", meetingUrl = GetURL(), startTime = new DateTime(2022, 12, 12, 1, 15, 15, DateTimeKind.Utc), endTime = new DateTime(2022, 12, 12, 1, 25, 20, DateTimeKind.Utc), eventName ="Town Hall"},
            new Appointment{ ID = new Guid("3b5ec916-eb18-41a3-9a21-4524f0fcf521"), Name = "Pavatharini", meetingUrl = GetURL(), startTime = new DateTime(2022, 12, 5, 1, 0, 30, DateTimeKind.Utc), endTime = new DateTime(2022, 12, 12, 1, 10, 10, DateTimeKind.Utc), eventName ="1 on 1"},
        };

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await Task.FromResult(appointmentDetails);
        }

        public async Task<Appointment> GetDataByID(Guid id)
        {
            var resultByID = await Task.FromResult(appointmentDetails.Where(meetDetails => meetDetails.ID==id).FirstOrDefault());
            if(resultByID == null)
            {
                return null;
            }
            return resultByID;
        }

         public async Task<List<Appointment>> GetDataByEventName(string eventName)
        {
            List<Appointment> resultByName = new List<Appointment>();
            foreach(var meetDetails in appointmentDetails)
            {
                if(meetDetails.eventName.Equals(eventName))
                {
                    resultByName.Add(meetDetails);
                }
            }
            return await Task.FromResult(resultByName);
        }

           public async Task<Appointment> Postappointment(Appointment data)
        {
            appointmentDetails.Add(data);
            return await Task.FromResult(data);
        }

        public async Task<Appointment> UpdateAppoinmentDetail(Appointment data)
        {
            Appointment meetingDetail = data;
            if (data != null)
            {
                meetingDetail = appointmentDetails.Where(student => student.ID == data.ID).First();
                meetingDetail.ID = data.ID;
                meetingDetail.Name = data.Name;
                meetingDetail.meetingUrl = data.meetingUrl;
                meetingDetail.startTime = data.startTime;
                meetingDetail.endTime = data.endTime;
                meetingDetail.eventName = data.eventName;
            }
            return await Task.FromResult(meetingDetail);
        }

        public async Task<Appointment> DeleteStudentData(Guid id)
        {
            var deletedResult = appointmentDetails.Where(student => student.ID == id).First();
            appointmentDetails.Remove(deletedResult);
            return await Task.FromResult(deletedResult);
        }
    }
}
