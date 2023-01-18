using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using DisprzTraining.DataAccess;
using DisprzTraining.Business;
using DisprzTraining.Models;
using Moq;

namespace DisprzTraining.Tests.UnitTesting
{
    public class UnitTestDAL
    {
        // private readonly AppointmentBL _appoinmentBL;
        static IAppoinmentDAL appoinmentDAL = new AppoinmentDAL();

        // [Fact]
        // public async Task Get_Appoinments_ByID_DAL()
        // {
        //     //ARRANGE
        //     var ID = new Guid("766fdce0-7e9c-4c43-b068-02fd99c008d5");
        //     //ACT
        //     var appoinmentByID = await appoinmentDAL.GetDataByID(ID);
        //     //ASSERT
        //     Assert.Equal("Kanishka", appoinmentByID.Name);
        // }

        [Fact]
        public async Task Get_All_Appoinments_Returns_Count_DAL()
        {
            var resultAllAppoinments = await appoinmentDAL.GetAllAppointments();
            Assert.Equal(4, resultAllAppoinments.Count);
        }

        // [Fact]
        // public async Task Get_ByID_Returns_Null_DAL()
        // {
        //     var appoimentByID = await appoinmentDAL.GetDataByID(Guid.Empty);
        //     Assert.Equal(null, appoimentByID);
        // }

        // [Fact]
        // public async Task Get_By_Event_Name_Returns_Appoinments_DAL()
        // {
        //     var appoinmentByEvent = await appoinmentDAL.GetDataByEventName("Scrumcall");
        //     Assert.Equal(2, appoinmentByEvent.Count);
        // }
        [Fact]
        public async Task Post_Result_CheckBy_Eventname_DAL()
        {
            var url = "https://api/appoinments/fghrfdbrgn";
            string format = "MMM ddd d HH:mm yyyy";
            DateTime timestart1 = new DateTime(2022, 12, 31, 8, 10, 20, DateTimeKind.Utc);
            DateTime timeend1 =new DateTime(2022, 12, 31, 9, 00, 00, DateTimeKind.Utc);
            var meetingDetails = new Appointment()
            {
                ID = Guid.NewGuid(),
                Name = "Devasangeetha",
                meetingUrl = url,
                startTime = timestart1,
                endTime = timeend1,
                title = "Integration Testing"
            };
            var postResult = await appoinmentDAL.Postappoinment(meetingDetails);
            Assert.Equal("Integration Testing", postResult.title);
        }

        [Fact]
        public async Task UpdateBy_ID_Check_By_EventName_DAL()
        {
            //ARRANGE
            var url = "https://api/appoinments/fghrfdbrgn";
            string format = "MMM ddd d HH:mm yyyy";
            DateTime timestart1 = new DateTime(2022, 12, 31, 5, 10, 20, DateTimeKind.Utc);
            DateTime timeend1 =new DateTime(2022, 12, 31, 6, 00, 00, DateTimeKind.Utc);
            var meetingDetails = new Appointment()
            {
                ID = new Guid("d780857c-2df6-4b12-b484-97b75db63215"),
                Name = "Devasangeetha",
                meetingUrl = url,
                startTime = timestart1,
                endTime = timeend1,
                title = "Integration Testing"
            };
            // Act
            var result = await appoinmentDAL.UpdateAppoinmentDetail(meetingDetails);

            // Assert
            // Assert.Equal(resultBeforeUpdate.startTime,result.startTime);
            Assert.NotEqual("Scrumcall",result.title);
        }

        [Fact]
        public async Task Delete_By_ID_Returns_Success_Count_DAL()
        {
            var ID = new Guid("d780857c-2df6-4b12-b484-97b75db63215");
            var resultafterDeleted = await appoinmentDAL.DeleteStudentData(ID);
            var countAfterDeleted = await appoinmentDAL.GetAllAppointments();
            Assert.Equal(3,countAfterDeleted.Count);
        }
    }
}