using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using CarFactory.Enum;
using CarFactory.Models;
using CarFactory_Domain;
using CarFactory_Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarFactory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarFactory _carFactory;
        public CarController(ICarFactory carFactory)
        {
            _carFactory = carFactory;
        }

        [ProducesResponseType(typeof(BuildCarOutputModel), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<object> Post([FromBody][Required] BuildCarInputModel carsSpecs)
        {

            var wantedCars = TransformToDomainObjects(carsSpecs);
            //Build cars
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var cars = await _carFactory.BuildCarsAsync(wantedCars);
            stopwatch.Stop();

            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            return JsonConvert.SerializeObject(new BuildCarOutputModel
            {
                Cars = cars,
                RunTime = stopwatch.ElapsedMilliseconds
            }, settings);
        }

        private static IEnumerable<CarSpecification> TransformToDomainObjects(BuildCarInputModel carsSpecs)
        {
            //Check and transform specifications to domain objects
            var wantedCars = new List<CarSpecification>();
            foreach (var spec in carsSpecs.Cars)
            {
                if (spec.Specification.NumberOfDoors % 2 == 0)
                    throw new ArgumentException("Must give an odd number of doors");

                PaintJob? paint = null;
                var baseColor = Color.FromName(spec.Specification.Paint.BaseColor);
                paint = spec.Specification.Paint.Type.ToLower() switch
                {
                    "single" => new SingleColorPaintJob(baseColor),
                    "stripe" => new StripedPaintJob(baseColor, Color.FromName(spec.Specification.Paint.StripeColor)),
                    "dot" => new DottedPaintJob(baseColor, Color.FromName(spec.Specification.Paint.DotColor)),
                    _ => throw new ArgumentException($"Unknown paint type {spec.Specification.Paint.Type}"),
                };
                var dashboardSpeakers = spec.Specification.FrontWindowSpeakers?.Select(s => new CarSpecification.SpeakerSpecification { IsSubwoofer = s.IsSubwoofer })
                    ?? Array.Empty<CarSpecification.SpeakerSpecification>();
                var doorSpeakers = spec.Specification.DoorSpeakers?.Select(s => new CarSpecification.SpeakerSpecification { IsSubwoofer = s.IsSubwoofer })
                    ?? Array.Empty<CarSpecification.SpeakerSpecification>();

                var wantedCar = new CarSpecification(paint, spec.Specification.Manufacturer, spec.Specification.NumberOfDoors, doorSpeakers, dashboardSpeakers);

                for (var i = 1; i <= spec.Amount; i++)
                {
                    wantedCars.Add(wantedCar);
                }
            }
            return wantedCars;
        }
    }
}
