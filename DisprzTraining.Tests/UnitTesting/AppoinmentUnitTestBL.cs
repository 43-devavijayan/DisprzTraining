using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisprzTraining.Business;
using DisprzTraining.DataAccess;
using DisprzTraining.Controllers;
using Xunit;
using DisprzTraining.Models;

namespace DisprzTraining.Tests.UnitTesting
{
    public class AppoinmentUnitTestBL
    {
        static IAppoinmentDAL appoinmentDAL = new AppoinmentDAL();
        static IAppoinmentBL appoinmentBL = new AppointmentBL(appoinmentDAL);
        AppoinmentController appoinment = new(appoinmentBL);


        [Fact]
        public void Get_Businesslayer_NUllException()
        {
            // Act + ASSERT
            var result = Assert.ThrowsAsync<InvalidOperationException>(() => appoinmentBL.GetAppointments(null,null,""));

        }

        [Fact]
        public void GetByEventName_Businesslayer_NUllException()
        {
            var eventName = "DailyScrum";
            // Act + ASSERT
            var result = Assert.ThrowsAsync<NullReferenceException>(() => appoinmentBL.GetAppointments(null,null,eventName));

        }


        [Fact]
        public void Post_NullValue_Returns_NullException()
        {
            // Act + ASSERT
            var result = Assert.ThrowsAsync<NullReferenceException>(() => appoinmentBL.CreateAppoinment(null));

        }

        [Fact]
        public void Update_Null_BLReturns_NullException()
        {
            // Act + ASSERT
            var result = Assert.ThrowsAsync<NullReferenceException>(() => appoinmentBL.UpdateStudent(null));

        }

        [Fact]
        public void DeleteBy_ID_Null_BLReturns_NUllException()
        {
            var id = new Guid("4d0097f2-fef5-48a3-81d9-44484e50e9ad");
            // Act + ASSERT
            var result = Assert.ThrowsAsync<InvalidOperationException>(() => appoinmentBL.DeleteStudent(id));

        }

        //// FLAG APPOINMENT -TEST
        [Fact]
        public async Task FlagAppoinment_existing_meetingPresence_return_false()
        {
            //ARRANGE
            var url = "https://api/appoinments/fghrfdbrgn";
            string format = "MMM ddd d HH:mm yyyy";
            // DateTime timestart1 = new DateTime(2022, 12, 12, 1, 15, 15, DateTimeKind.Utc);
            // DateTime timeend1 = new DateTime(2022, 12, 12, 1, 25, 20, DateTimeKind.Utc);
            var meetingDetails = new Appointment()
            {
                ID = new Guid("d780857c-2df6-4b12-b484-97b75db63215"),
                Name = "Devasangeetha",
                meetingUrl = url,
                startTime = new DateTime(2022, 12, 12, 1, 15, 15, DateTimeKind.Utc),
                endTime = new DateTime(2022, 12, 12, 1, 25, 20, DateTimeKind.Utc),
                title = "Scrum call"
            };
            //ACT
            bool flagResult = await appoinmentBL.FlagAppoinment(meetingDetails);
            //ASSERT
            Assert.Equal(flagResult, false);

        }
    }
}