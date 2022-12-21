using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace XUnitTests
{
    public class UnitTest1
    {

        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7126") };

        //public static class TestHelpers
        //{
        //    private const string _jsonMediaType = "application/json";
        //    private const int _expectedMaxElapsedMilliseconds = 1000;
        //   // private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
        //}
        [Fact]
        public async Task Test1()
        {
            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
           
            var stopwatch = Stopwatch.StartNew();
            // Act.
            var response = await _httpClient.GetAsync("/books");
            // Assert.
            //await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode,"");

            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

    
    }
}