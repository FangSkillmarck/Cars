using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CarFactory_Domain;
using CarFactory_Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public object Post([FromBody][Required] BuildCarInputModel carsSpecs)
        {

            var wantedCars = TransformToDomainObjects(carsSpecs);
            //Build cars
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var cars = _carFactory.BuildCars(wantedCars);
            stopwatch.Stop();

            //Create response and return
            return new BuildCarOutputModel {
                Cars = cars,
                RunTime = stopwatch.ElapsedMilliseconds
            };
        }

        private static IEnumerable<CarSpecification> TransformToDomainObjects(BuildCarInputModel carsSpecs)
        {
            //Check and transform specifications to domain objects
            var wantedCars = new List<CarSpecification>();
            try
            {
                foreach (var spec in carsSpecs.Cars)
                {
                for (var i = 1; i <= spec.Amount; i++)
                    {
                        if (spec.Specification.NumberOfDoors % 2 == 0)
                        {
                            throw new BadHttpRequestException("The number of doors is 3 or 5");
                        }
                        PaintJob paint = null;
                        var baseColorInput = spec.Specification.Paint.BaseColor.Trim();
                        var baseColor = ParseColor(baseColorInput);

                        switch (spec.Specification.Paint.Type.ToLower().Trim())
                        {
                            case "single":
                                paint = new SingleColorPaintJob(baseColor);
                                break;
                            case "stripe":
                                paint = new StripedPaintJob(baseColor, ParseColor(spec.Specification.Paint.StripeColor));
                                break;
                            case "dot":
                                paint = new DottedPaintJob(baseColor, ParseColor(spec.Specification.Paint.DotColor));
                                break;
                            default:
                                throw new BadHttpRequestException(string.Format("Unknown paint type, the paint type should be \"single\", \"stripe\" or \"dot\" ", spec.Specification.Paint.Type));
                        }
                        Manufacturer manufacturer = spec.Specification.Manufacturer;
                        var dashboardSpeakers = spec.Specification.FrontWindowSpeakers.Select(s => new CarSpecification.SpeakerSpecification { IsSubwoofer = s.IsSubwoofer });
                        var doorSpeakers = new CarSpecification.SpeakerSpecification[0]; //TODO: Let people install door speakers
                        var wantedCar = new CarSpecification(paint, manufacturer, spec.Specification.NumberOfDoors, doorSpeakers, dashboardSpeakers);
                        wantedCars.Add(wantedCar);
                    }
                } 
            }
            catch (BadHttpRequestException ex)
            {
                Console.WriteLine("Exception Message: " + ex.Message);
                throw;
            }
            return wantedCars;
        }

        //Parseing color input and make the first letter in colors to uppercase, 
        public static Color ParseColor(string colorInput)
        {
            if (String.IsNullOrEmpty(colorInput))
            {
                throw new BadHttpRequestException("The value of the color is needed");
            }
            colorInput = colorInput.First().ToString().ToUpper() + colorInput.Substring(1);
            var baseColor = Color.FromName(colorInput);
            return baseColor;
        }

        public class BuildCarInputModel
        {
            public IEnumerable<BuildCarInputModelItem> Cars { get; set; }
        }

        public class BuildCarInputModelItem
        {
            [Required]
            public int Amount { get; set; }
            [Required]
            public CarSpecificationInputModel Specification { get; set; }
        }

        public class CarPaintSpecificationInputModel
        {
            public string Type { get; set; }
            public string BaseColor { get; set; }
            public string StripeColor { get; set; }
            public string DotColor { get; set; }
        }

        public class CarSpecificationInputModel
        {
            public int NumberOfDoors { get; set; }
            public CarPaintSpecificationInputModel Paint { get; set; }
            public Manufacturer Manufacturer { get; set; }
            public SpeakerSpecificationInputModel[] FrontWindowSpeakers { get; set; }
        }

        public class SpeakerSpecificationInputModel
        {
            public bool IsSubwoofer { get; set; }
        }

        public class BuildCarOutputModel{
            public long RunTime { get; set; }
            public IEnumerable<Car> Cars { get; set; }
        }
    }
}
