using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
//using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

namespace integrationtests
{
    public class PersonControllerTests
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [Test]
        [TestCase("GetPerson/16")]
        [TestCase("GetPerson/17")]
        [TestCase("GetPerson/18")]
        public async Task GetPerson_WithQueryParameters_ReturnsOk(string queryParams)
        {
            var response = await _client.GetAsync($"/Person/{queryParams}"); // otherwise returns NotFound();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task CreatePerson_WithValidModel_ReturnsOk()
        {
            var person = new PersonModel()
            {
                Name = "Maria"
            }; // won't post valid data because my method isn't configured for this
            var json = JsonConvert.SerializeObject(person);
            //var httpContent = new StringContent(JsonSerializer.Serialize(person));
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/Person/Create", httpContent);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK); // all my controller methods return OK status
        }
    }
}