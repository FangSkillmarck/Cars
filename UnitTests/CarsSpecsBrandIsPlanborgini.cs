using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Paint;
using FluentAssertions;
using System.Drawing;
using static CarFactory.Controllers.CarController;
using CarFactory.Controllers;

//To test Carcontroller's POST method , Post Json body is:
//        {
//  "cars": [
//    {
//      "amount": 15,
//      "specification": {
//        "numberOfDoors": 3,
//        "paint": {
//          "type": "dot",
//          "baseColor": "red",
//          "stripeColor": null,
//          "dotColor": "pink"
//        },
//        "manufacturer": "Planborghini",
//        "frontWindowSpeakers": [
//          {
//            "isSubwoofer": true
//          }
//        ]
//      }
//    }
//  ]
//}
// |PlanfaRomeo|3 doors|Pink base with red dots|10 subwoofers and 20 standard|15| if it is a PlanfaRomeo than 3 doors, pink base and red sdots

namespace UnitTests
{
    [TestClass]
    public class CarsSpecsBrandIsPlanfaRomeo
    {
        //Arange, CarsSpecsBrand_PlanfaRomeo
        public static CarPaintSpecificationInputModel spec = new CarPaintSpecificationInputModel
        {
            Type = "Stripe ",
            BaseColor = "Blue",
            StripeColor = "Orange",
            DotColor = null
        };

        public static CarSpecificationInputModel inputModel = new CarSpecificationInputModel
        {
            NumberOfDoors = 3,
            Paint = spec,
            Manufacturer = Manufacturer.Planborghini
        };

        public static BuildCarInputModelItem carSpecItem = new BuildCarInputModelItem
        {
            Amount = 15,
            Specification = inputModel
        };

        //public static BuildCarInputModel carsSepec = new BuildCarInputModel
        //{
        //    Cars = [{carsSepecItem}]
        //};

        [TestMethod]
        public void PaintType_Stripe_With_Capital_Letter_Should_Work_As_Small_lettter_Stripe()
        {
            var paintType = spec.Type;
            var painterJob = new StripedPaintJob(ParseColor(spec.BaseColor), ParseColor(spec.StripeColor));
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.PlanfaRomeo, 5);
            painter.PaintCar(car, painterJob);
            var job = (StripedPaintJob)car.PaintJob;
            job.Should().NotBeNull();
        }

        [TestMethod]
        public void CarsSpecsBrand_PlanfaRomeo_Should_Have_Quantity_75()
        {

        }

        [TestMethod]
        public void CarsSpecsBrand_PlanfaRomeo_Should_Have_Three_Doors()
        {
         
        }

        [TestMethod]
        public void CarsSpecsBrand_PlanfaRomeo_Should_Have_Paint_With_Blue_Base()
        {
            var painterJob = new StripedPaintJob(ParseColor(spec.BaseColor), ParseColor(spec.StripeColor));
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.PlanfaRomeo, 5);
            painter.PaintCar(car, painterJob);
            var job = (StripedPaintJob)car.PaintJob;
            job.BaseColor.Should().Be(Color.Blue);
        }

        [TestMethod]
        public void CarsSpecsBrand_PlanfaRomeo_Should_Have_Paint_With_Orange_Stripes()
        {
        var painterJob = new StripedPaintJob(ParseColor(spec.BaseColor), ParseColor(spec.StripeColor));
        var painter = new Painter();
        var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.PlanfaRomeo, 5);
        painter.PaintCar(car, painterJob);
        var job = (StripedPaintJob)car.PaintJob;
        job.StripeColor.Should().Be(Color.Orange);
         }

        [TestMethod]
        public void CarsSpecsBrand_PlanfaRomeo_Should_Have_Paint_With_No_Stripe()
        {
        }
    }
}
