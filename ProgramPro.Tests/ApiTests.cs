using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ProgramPro.Shared.Models;
using System.Net.Http.Json;
using System.Net.Http;

namespace IntegrationTests
{
    public class ApiTests
    {
        private readonly HttpClient _client;

        public ApiTests()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7134");
            _client.DefaultRequestHeaders.Add("ApiKey", "ProgramPro_#DONT#TOUCH#THIS.IS^^VERY**-12394827523235123.23.423,2134#RESTRICTED_API.Key");
        }

        [Fact]
        public async Task CanCreateNewTrainingProgram()
        {
            // Act
            var response = await _client.PostAsJsonAsync("api/Trainingprograms", new TrainingProgram { Name = "New Program", StartDate = DateTime.Now });

            // Assert
            response.EnsureSuccessStatusCode(); // Ensure the HTTP request was successful (status code 200-299).
                                                // Add more assertions here to validate the response content or other aspects of the test.
        }

        [Fact]
        public async Task CanGetTrainingPrograms()
        {
            // Act
            var response = await _client.GetAsync("api/Trainingprograms");

            // Assert
            Assert.True(response.IsSuccessStatusCode); // Ensure the HTTP request was successful (status code 200-299).
                                                // Add more assertions here to validate the response content or other aspects of the test.
        }
    }
}
