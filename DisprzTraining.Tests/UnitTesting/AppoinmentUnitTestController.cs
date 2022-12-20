using System;
using DisprzTraining.Business;
using DisprzTraining.Controllers;
using DisprzTraining.DataAccess;
using DisprzTraining.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DisprzTraining.Tests.UnitTesting
{
    public class UnitTestController
    {
        static IAppoinmentDAL appoinmentDAL = new AppoinmentDAL();
        static IAppoinmentBL appoinmentBL = new AppointmentBL(appoinmentDAL);
        AppoinmentController appoinment = new(appoinmentBL);


        ////// GET ALL DATA - TESTCASES

        [Fact]
        public async Task GetAllAppoinment_Returns_200_Success()
        {
            // Act
            var result = await appoinment.GetAppointmentDetails() as OkObjectResult;

            // Assert
            Assert.Equal(200, result?.StatusCode);
        }

        [Fact]
        public async void GetAllAppoinment_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = await appoinment.GetAppointmentDetails() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Appointment>>(okResult.Value);
            Assert.Equal(4, items.Count);
        }

        /////GET BY ID - TESTCASES

        [Fact]
        public async Task GetByID_Returns_200_Success()
        {
            //ARRANGE
            var id = new Guid("d780857c-2df6-4b12-b484-97b75db63215");

            // Act
            var result = await appoinment.GetAppointmentByID(id) as OkObjectResult;

            // Assert
            Assert.Equal(200, result?.StatusCode);
        }

        [Fact]
        public async void GetByID_WhenCalled_Return_Appoinment()
        {
            //ARRANGE 
            var id = new Guid("d780857c-2df6-4b12-b484-97b75db63215");

            // Act
            var okResult = await appoinment.GetAppointmentByID(id) as OkObjectResult;
            var items = okResult.Value as Appointment;

            // Assert
            Assert.True(items.Name.Equals("Devasangeetha"));
        }

        [Fact]
        public async Task GetByID_Result_404_NotFound()
        {
            //Act
            var id = new Guid("4d0097f2-fef5-48a3-81d9-44484e50e9ad");
            var resultStatus = await appoinment.GetAppointmentByID(id) as NotFoundResult;

            //Assert
            Assert.Equal(404, resultStatus?.StatusCode);
        }

        [Fact]
        public async Task GetByID_Null_Result_BadRequest()
        {
            //Act
            var resultStatus = await appoinment.GetAppointmentByID(Guid.Empty);
            var test = Assert.IsType<BadRequestObjectResult>(resultStatus);

            //Assert
            Assert.Equal(400, test?.StatusCode);
        }

        ////GET BY EVENTNAME -TESTCASES

        [Fact]
        public async Task GetByEventName_Returns_200_Success()
        {
            // Act
            var result = await appoinment.GetAppointmentByEventName("1 on 1") as OkObjectResult;

            // Assert
            Assert.Equal(200, result?.StatusCode);
        }

        [Fact]
        public async void GetByEventName_WhenCalled_Return_Appoinment()
        {
            // Act
            var okResult = await appoinment.GetAppointmentByEventName("Scrumcall") as OkObjectResult;

            var items = Assert.IsType<List<Appointment>>(okResult.Value);
            // Assert
            Assert.True(items.Count == 2);
        }

        [Fact]
        public async Task GetByEventName_Result_404_NotFound()
        {
            var eventName = "DailyScrum";
            var resultStatus = await appoinment.GetAppointmentByEventName(eventName) as NotFoundResult;

            //Assert
            Assert.Equal(404, resultStatus?.StatusCode);
            Assert.IsType<NotFoundResult>(resultStatus);
        }

        [Fact]
        public async Task GetByEventName_Null_Result_BadRequest()
        {
            // Act
            var badResponse = await appoinment.GetAppointmentByEventName(string.Empty);
            var test = Assert.IsType<BadRequestObjectResult>(badResponse);
            // Assert
            Assert.Equal(400, test?.StatusCode);
        }



        ///// POST APPOINMENT - TESTCASES

        [Fact]
        public async Task Post_Result_Success_CheckBy_Name()
        {
            var url = "https://api/appoinments/fghrfdbrgn";
            var meetingDetails = new Appointment()
            {
                ID = Guid.NewGuid(),
                Name = "Devasangeetha",
                meetingUrl = url,
                startTime = new DateTime(2022, 12, 31, 8, 10, 20, DateTimeKind.Utc),
                endTime = new DateTime(2022, 12, 31, 9, 00, 00, DateTimeKind.Utc),
                eventName = "Scrum call"
            };
            var postResult = await appoinment.PostAppointmentDetails(meetingDetails) as CreatedResult;
            Assert.True(postResult.Value.Equals(true));
        }

        [Fact]
        public async Task Post_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Act
            var badResponse = await appoinment.PostAppointmentDetails(null) as BadRequestObjectResult;

            // Assert
            Assert.Equal(400, badResponse?.StatusCode);
        }

        [Fact]
        public async Task Post_Result_Value_Check_success_201()
        {

            //ARRANGE
            var url = "https://api/appoinments/fghrfdbrgn";
            var meetingDetails = new Appointment()
            {
                ID = Guid.NewGuid(),
                Name = "Devasangeetha",
                meetingUrl = url,
                startTime = new DateTime(2022, 12, 31, 6, 30, 20, DateTimeKind.Utc),
                endTime = new DateTime(2022, 12, 31, 7, 00, 00, DateTimeKind.Utc),
                eventName = "Scrum call"
            };

            //ACT
            var postResult = await appoinment.PostAppointmentDetails(meetingDetails) as CreatedResult;

            //ASSERT
            Assert.Equal(201, postResult?.StatusCode);
        }

        [Fact]
        public async Task Post_Result_Value_Check_Conflict_409()
        {
            //ARRANGE
            var url = "https://api/appoinments/fghrfdbrgn";
            var meetingDetails = new Appointment()
            {
                ID = Guid.NewGuid(),
                Name = "Devasangeetha",
                meetingUrl = url,
                startTime = new DateTime(2022, 12, 31, 5, 10, 20, DateTimeKind.Utc),
                endTime = new DateTime(2022, 12, 31, 6, 00, 00, DateTimeKind.Utc),
                eventName = "Scrum call"
            };

            //ACT
            var postResult = await appoinment.PostAppointmentDetails(meetingDetails) as ConflictObjectResult;

            //ASSERT
            Assert.Equal(409, postResult?.StatusCode);
        }


        //// PUT - TESTCASES

        [Fact]
        public async Task UpdateBy_ID_Returns_200_Success()
        {
            //ARRANGE
            var url = "https://api/appoinments/fghrfdbrgn";
            var meetingDetails = new Appointment()
            {
                ID = new Guid("d780857c-2df6-4b12-b484-97b75db63215"),
                Name = "Devasangeetha",
                meetingUrl = url,
                startTime = new DateTime(2022, 12, 31, 5, 10, 20, DateTimeKind.Utc),
                endTime = new DateTime(2022, 12, 31, 6, 00, 00, DateTimeKind.Utc),
                eventName = "Updates"
            };

            // Act
            var result = await appoinment.UpdateStudentDetails(meetingDetails) as OkObjectResult;

            // Assert
            var resultSuccess = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, resultSuccess?.StatusCode);
        }

        [Fact]
        public async Task Update_InvalidObject_ReturnsBadRequest()
        {
            // Act
            var badResponse = await appoinment.UpdateStudentDetails(null) as BadRequestObjectResult;

            // Assert
            // Assert.IsType<BadRequestObjectResult>(badResponse);
            Assert.Equal(400, badResponse?.StatusCode);
        }

        [Fact]
        public async Task Update_Result_Value_Check_Conflict_409()
        {
            //ARRANGE
            var url = "https://api/appoinments/fghrfdbrgn";
            var meetingDetails = new Appointment()
            {
                ID = new Guid("d780857c-2df6-4b12-b484-97b75db63215"),
                Name = "Devasangeetha",
                meetingUrl = url,
                startTime = new DateTime(2022, 12, 12, 1, 15, 15, DateTimeKind.Utc),
                endTime = new DateTime(2022, 12, 12, 1, 25, 20, DateTimeKind.Utc),
                eventName = "Scrum call"
            };
            //ACT
            var updateResult = await appoinment.UpdateStudentDetails(meetingDetails) as ConflictObjectResult;

            //ASSERT
            Assert.Equal(409, updateResult?.StatusCode);
        }

        //// DELETE - TESTCASES

        [Fact]
        public async Task DeleteBy_ID_WrongID_Result_404_NotFound()
        {
            //Act
            var id = new Guid("4d0097f2-fef5-48a3-81d9-44484e50e9ad");
            var resultStatus = await appoinment.DeleteStudentDetails(id) as NotFoundResult;

            //Assert
            Assert.Equal(404, resultStatus?.StatusCode);
            Assert.IsType<NotFoundResult>(resultStatus);
        }

        [Fact]
        public async Task DeleteBy_ID_Null_Result_BadRequest()
        {
            //Act
            var resultStatus = await appoinment.DeleteStudentDetails(Guid.Empty);

            //Assert
            var resultBadRequest = Assert.IsType<BadRequestObjectResult>(resultStatus);
            Assert.Equal(400, resultBadRequest?.StatusCode);
        }

        [Fact]
        public async Task DeleteBY_ID_ExistingGuidPassed_NoContentResult()
        {
            var existingGuid = new Guid("766fdce0-7e9c-4c43-b068-02fd99c008d5");
            // Act
            var noContentResponse = await appoinment.DeleteStudentDetails(existingGuid);
            // Assert
            Assert.IsType<NoContentResult>(noContentResponse);
        }

        [Fact]
        public async Task DeleteBY_ID_ExistingGuidPassed_GetAllItemCount()
        {
            var existingGuid = new Guid("766fdce0-7e9c-4c43-b068-02fd99c008d5");
            // Act
            var okResult = await appoinment.DeleteStudentDetails(existingGuid);
            var noContentResponse = await appoinment.GetAppointmentDetails() as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Appointment>>(noContentResponse.Value);
            Assert.Equal(3, items.Count);
        }
    }
}