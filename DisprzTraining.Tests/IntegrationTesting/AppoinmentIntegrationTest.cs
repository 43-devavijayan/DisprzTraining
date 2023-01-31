using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DisprzTraining.Controllers;
using DisprzTraining.Business;
using DisprzTraining.DataAccess;
using DisprzTraining.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Net.Http.Headers;

namespace DisprzTraining.Tests
{
    public class AppoinmentIntegrationTest
    {
        static IAppoinmentDAL appoinmentDAL = new AppoinmentDAL();
        static IAppoinmentBL appoinmentBL = new AppointmentBL(appoinmentDAL);
        AppoinmentController appoinment = new(appoinmentBL);
        private readonly HttpClient _client;

        public AppoinmentIntegrationTest()
        {
            var integrationAppoinment = new WebApplicationFactory<AppoinmentController>();
            _client = integrationAppoinment.CreateClient();

        }
        /////INTEGRATION TEST - GETALL        

        [Fact]
        public async Task Integration_Testing_Get_All()
        {
            var response = await _client.GetAsync("http://localhost:5169/api/appoinment");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task Integration_Testing_GetByStartTime()
        {
            var response = await _client.GetAsync("http://localhost:5169/api/appoinment?startTime=2022-12-12T01%3A15%3A15Z");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task Integration_Testing_GetByEndTime()
        {
            var response = await _client.GetAsync("http://localhost:5169/api/appoinment?endTime=2022-12-12T01%3A55%3A20Z");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task Integration_Testing_GetByTitle()
        {
            var response = await _client.GetAsync("http://localhost:5169/api/appoinment?title=Scrumcall");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        /////INTEGRATION TEST - GET BY ID      

        // [Fact]
        // public async Task Integration_Testing_Get_Appoinment_ByID()
        // {
        //     var response = await _client.GetAsync("http://localhost:5169/api/appoinment/ID?id=d780857c-2df6-4b12-b484-97b75db63215");

        //     response.EnsureSuccessStatusCode();

        //     Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // }

        /////INTEGRATION TEST - GET BY EVENT NAME     

        // [Fact]
        // public async Task Integration_Testing_Get_Appoinment_ByEventName()
        // {
        //     var response = await _client.GetAsync("http://localhost:5169/api/appoinment/Event?eventName=Scrumcall");

        //     response.EnsureSuccessStatusCode();

        //     Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // }

        ////INTEGRATION TEST - POST 

        [Fact]
        public async Task Integration_Testing_Post_Appoinment()
        {
            // Arrange
            var Url = "http://localhost:5169/api/appoinment/Post";
            string format = "MMM ddd d HH:mm yyyy";
            DateTime timestart1 = new DateTime(2023, 12, 31, 2, 10, 20, DateTimeKind.Utc);
            DateTime timeend1 = new DateTime(2023, 12, 31, 2, 20, 00, DateTimeKind.Utc);
            var meetingDetails = new Appointment
            {
                Name = "Kanishka",
                meetingUrl = "https://api/appoinment/dfnbhgteyudbjn5",
                start = timestart1,
                end = timeend1,
                title = "Integration Testing"
            };
            var jsonString = JsonSerializer.Serialize(meetingDetails);
            var httpRequestMessage = new HttpRequestMessage
            {
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
            };
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponse = await _client.PostAsync(Url, httpRequestMessage.Content);
            // Assert
            httpResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Integration_Testing_Update_Appoinment()
        {
            // Arrange
            var Url = "http://localhost:5169/api/appoinment/ID";
            // string format = "MMM ddd d HH:mm yyyy";
            DateTime timestart1 = new DateTime(2023, 12, 31, 2, 10, 20, DateTimeKind.Utc);
            DateTime timeend1 = new DateTime(2023, 12, 31, 2, 20, 00, DateTimeKind.Utc);
            var meetingDetails = new Appointment
            {
                ID = new Guid("766fdce0-7e9c-4c43-b068-02fd99c008d5"),
                Name = "Kanishka",
                meetingUrl = "https://api/appoinment/dfnbhgteyudbjn5",
                start = timestart1,
                end = timeend1,
                title = "Integration Testing"
            };
            var jsonString = JsonSerializer.Serialize(meetingDetails);
            var httpRequestMessage = new HttpRequestMessage
            {
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
            };
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponse = await _client.PutAsync(Url, httpRequestMessage.Content);
            httpResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Integration_Testing_Delete_Appoinment()
        {
            // Arrange
            var Url = "http://localhost:5169/api/appoinment/ID?id=766fdce0-7e9c-4c43-b068-02fd99c008d5";
            var httpResponse = await _client.DeleteAsync(Url);
            httpResponse.EnsureSuccessStatusCode();
        }
    }
}