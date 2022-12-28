using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using asp;
namespace integrationtests
{
    public class PersonControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("GetPerson/16")]
        [TestCase("GetPerson/17")]
        [TestCase("GetPerson/18")]
        public async Task GetUser_WithQueryParameters_ReturnsOk(string queryParams)
        {
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();
            var response = await client.GetAsync($"/Person/{queryParams}"); // otherwise returns NotFound();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}