using System;
using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Timelogger.Api.Tests.Factory;
using Timelogger.Models;

namespace Timelogger.Api.Tests
{
    public class ProjectsControllerTests
    {
        private WebApiFactory factory;
        private HttpClient client;

        [OneTimeSetUp]
        public void Setup()
        {
            factory = new WebApiFactory();
            client = factory.CreateDefaultClient();
        }

        [Test]
        public async Task CreateProjectShouldBeReturned()
        {
            // Arrange
            DateTime deadline = DateTime.Now.AddDays(60).ToUniversalTime();
            string projectName = "TestProjectName";
            ProjectModel model = new ProjectModel()
            {
                DeadLine = deadline,
                Name = projectName
            };

            HttpContent content = JsonContent.Create(model, MediaTypeHeaderValue.Parse("application/json"));

            // Act
            var projectId = await (await client.PostAsync("api/Projects", content))
                .Content.ReadAsStringAsync();

            var resultGet = await client.GetStringAsync($"api/Projects/{projectId}");

            var projectModel = JsonConvert.DeserializeObject<ProjectModel>(resultGet);

            // Assert
            Assert.That(projectModel.DeadLine, Is.EqualTo(deadline));
            Assert.That(projectModel.Name, Is.EqualTo(projectName));
        }
    }
} 
