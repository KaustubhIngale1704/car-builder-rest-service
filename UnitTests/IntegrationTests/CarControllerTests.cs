using CarFactory.Models;
using CarFactory_Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarFactory.Tests.IntegrationTests
{
    public class CarControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        readonly HttpClient _client;

        public CarControllerTests(WebApplicationFactory<Startup> application)
        {
            _client = application.CreateClient();
        }

        [Fact]
        public async Task Scenario1_Test()
        {
            int total = 75;

            var model = CreateRequest(total, 1, 2, "blue", "stripe", "orange", 5, Manufacturer.PlanfaRomeo);

            HttpResponseMessage response = await SendRequestAsync(model);

            var body = await response.Content.ReadAsStringAsync();
            var outputModel = JsonConvert.DeserializeObject<BuildCarOutputModel>(body, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
            });

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            outputModel.Cars.Count().Should().Be(total);
        }

        [Fact]
        public async Task Scenario2_Test()
        {
            int total = 15;

            var model = CreateRequest(total, 10, 20, "pink", "dot", "red", 3, Manufacturer.Planborghini);

            HttpResponseMessage response = await SendRequestAsync(model);

            var body = response.Content.ReadAsStringAsync().Result;

            body.Should().Contain("More than 2 speakers aren't supported");
        }

        [Fact]
        public async Task Scenario3_Test()
        {
            int total = 20;

            var model = CreateRequest(total, 0, 4, "red", "stripe", "black", 5, Manufacturer.Volksday);

            HttpResponseMessage response = await SendRequestAsync(model);

            var body = response.Content.ReadAsStringAsync().Result;

            body.Should().Contain("Sequence contains no matching element");
        }

        [Fact]
        public async Task Scenario4_Test()
        {
            int total = 40;

            var model = CreateRequest(total, 1, 2, "black", "dot", "yellow", 3, Manufacturer.PlandayMotorWorks);

            HttpResponseMessage response = await SendRequestAsync(model);

            var body = await response.Content.ReadAsStringAsync();
            var outputModel = JsonConvert.DeserializeObject<BuildCarOutputModel>(body, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
            });

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            outputModel.Cars.Count().Should().Be(total);
        }

        [Fact]
        public async Task Scenario5_Test()
        {
            int total = 20;

            var model = CreateRequest(total, 0, 4, "green", "stripe", "gold", 5, Manufacturer.Plandrover);

            HttpResponseMessage response = await SendRequestAsync(model);

            var body = await response.Content.ReadAsStringAsync();
            var outputModel = JsonConvert.DeserializeObject<BuildCarOutputModel>(body, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
            });

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            outputModel.Cars.Count().Should().Be(total);
        }

        private async Task<HttpResponseMessage> SendRequestAsync(BuildCarInputModel model)
        {
            var request = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/car", request);
            return response;
        }

        private static BuildCarInputModel CreateRequest(
            int amount, 
            int subWoofers, 
            int standardSpeakers, 
            string baseColor, 
            string type, 
            string color, 
            int numberOfDoors, 
            Manufacturer manufacturer)
        {
            var doorSpeakers = new List<SpeakerSpecificationInputModel>();
            var frontSpeakers = new List<SpeakerSpecificationInputModel>();

            var totalSpeakers = subWoofers + standardSpeakers;

            var doorSpeakersCount = totalSpeakers / 2;

            var frontSpekaersCount = totalSpeakers - doorSpeakersCount;


            for (int i = 0; i < doorSpeakersCount; i++)
            {
                doorSpeakers.Add(new SpeakerSpecificationInputModel());
            }

            for (int i = 0; i < frontSpekaersCount; i++)
            {
                frontSpeakers.Add(new SpeakerSpecificationInputModel());
            }

            // Distribute subwoofers 
            var doorSubWoofers = subWoofers / 2;
            var frontSubWoofers = subWoofers - doorSubWoofers;
            for (int i = 0; i < doorSubWoofers; i++)
            {
                doorSpeakers[i].IsSubwoofer = true;
            }

            for (int i = 0; i < frontSubWoofers; i++)
            {
                doorSpeakers[i].IsSubwoofer = true;
            }

            return new BuildCarInputModel()
            {
                Cars = new List<BuildCarInputModelItem>()
                {
                    new BuildCarInputModelItem()
                    {
                        Amount = amount,
                        Specification = new CarSpecificationInputModel()
                        {
                            DoorSpeakers = doorSpeakers.ToArray(),
                            FrontWindowSpeakers = frontSpeakers.ToArray(),
                            Manufacturer = manufacturer,
                            NumberOfDoors = numberOfDoors,
                            Paint = new CarPaintSpecificationInputModel()
                            {
                                BaseColor = baseColor,
                                StripeColor = color,
                                Type = type,
                                DotColor = color
                            }
                        }
                    }
                }
            };
        }
    }
}
