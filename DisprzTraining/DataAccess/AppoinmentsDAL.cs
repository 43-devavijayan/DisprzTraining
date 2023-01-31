using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{
    public class AppoinmentDAL : IAppoinmentDAL
    {
        ////GENERATE URL FOR MEETING.
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

        // static string format = "MMM ddd d HH:mm yyyy";
        // static DateTime timestart1 = new DateTime(2022, 12, 31, 5, 10, 20, DateTimeKind.Utc);
        // // System.Console.WriteLine(timestart1);
        // // startTime =timestart2.ToString(format)
        // static DateTime timeend1 = new DateTime(2023, 12, 31, 5, 20, 0, DateTimeKind.Utc);
        // static DateTime timestart2 = new DateTime(2022, 12, 31, 1, 30, 00, DateTimeKind.Utc);
        // static DateTime timeend2 = new DateTime(2022, 12, 31, 2, 0, 0, DateTimeKind.Utc);
        // static DateTime timestart3 = new DateTime(2022, 12, 12, 1, 15, 15, DateTimeKind.Utc);
        // static DateTime timeend3 = new DateTime(2022, 12, 12, 1, 25, 20, DateTimeKind.Utc);
        // static DateTime timestart4 = new DateTime(2022, 12, 5, 1, 0, 30, DateTimeKind.Utc);
        // static DateTime timeend4 = new DateTime(2022, 12, 12, 1, 10, 10, DateTimeKind.Utc);
        public static List<Appointment> appointmentDetails = new(){

            new Appointment{ ID = new Guid("d780857c-2df6-4b12-b484-97b75db63215"), Name = "Devasangeetha", meetingUrl = GetURL(), start = new DateTime(2023, 1, 31, 5, 10, 20, DateTimeKind.Utc) , end = new DateTime(2023, 1, 31, 6, 40, 0, DateTimeKind.Utc), title ="Scrum call"},
            new Appointment{ ID = new Guid("766fdce0-7e9c-4c43-b068-02fd99c008d5"), Name = "Kanishka", meetingUrl = GetURL(), start =new DateTime(2023, 1, 31, 1, 30, 00, DateTimeKind.Utc) , end =new DateTime(2023, 1, 31, 2, 30, 0, DateTimeKind.Utc) , title ="Dev Test"},
            new Appointment{ ID = new Guid("ed46a787-9522-47cb-897a-96714b4c877e"), Name = "VijayShree", meetingUrl = GetURL(), start = new DateTime(2023, 1, 12, 1, 15, 15, DateTimeKind.Utc) , end =new DateTime(2023, 1, 12, 1, 55, 20, DateTimeKind.Utc) , title ="Town Hall"},
            new Appointment{ ID = new Guid("3b5ec916-eb18-41a3-9a21-4524f0fcf521"), Name = "Pavatharini", meetingUrl = GetURL(), start = new DateTime(2023, 1, 5, 1, 0, 0, DateTimeKind.Utc), end =new DateTime(2023, 1, 5, 1, 50, 0, DateTimeKind.Utc), title ="1 on 1"},
        };

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await Task.FromResult(appointmentDetails);
        }

        // public async Task<Appointment> GetDataByID(Guid id)
        // {
        //     var resultByID = await Task.FromResult(appointmentDetails.Where(meetDetails => meetDetails.ID == id).FirstOrDefault());
        //     if (resultByID == null)
        //     {
        //         return null;
        //     }
        //     return resultByID;
        // }

        // public async Task<List<Appointment>> GetDataByEventName(string eventName)
        // {
        //     List<Appointment> resultByName = new List<Appointment>();
        //     foreach (var meetDetails in appointmentDetails)
        //     {
        //         if (meetDetails.title.Equals(eventName))
        //         {
        //             resultByName.Add(meetDetails);
        //         }
        //     }
        //     return await Task.FromResult(resultByName);
        // }

        public async Task<Appointment> Postappoinment(Appointment data)
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
                meetingDetail.start = data.start;
                meetingDetail.end = data.end;
                meetingDetail.title = data.title;
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
