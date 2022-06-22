using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Paint;
using FluentAssertions;
using System.Drawing;
using static CarFactory.Controllers.CarController;
using CarFactory.Controllers;

//To test Carcontroller's POST method , Post Json body are:
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
// |Planborgini|3 doors|Pink base with red dots|10 subwoofers and 20 standard|15| if it is a Planborgini than 3 doors, pink base and red sdots

namespace UnitTests
{
    [TestClass]
    public class CarsSpecsBrandIsPlanborgini
    {
        //Arange, CarsSpecsBrand_Planborgini
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
        public void PaintType_Stripe_With_Capital_Letter_Should_Work_As_Small_lettter()
        {
            //var paintType = new CarPaintSpecificationInputModel().Type;
            var paintType = spec.Type;
            var painterJob = new StripedPaintJob(ParseColor(spec.BaseColor), ParseColor(spec.StripeColor));
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.Plandrover, 5);
            painter.PaintCar(car, painterJob);
            var job = (StripedPaintJob)car.PaintJob;
            job.StripeColor.Should().Be(Color.Orange);
        }

        public void CarsSpecsBrand_Planborgini_Should_Have_Quantity_15()
        {
            CarsSpecsBrand_Planborgini_Should_Have_Quantity_15();
        }

        [TestMethod]
        public void CarsSpecsBrand_Planborgini_Should_Have_Quantity_15(CarController carController)
        {

        }

        [TestMethod]
        public void CarsSpecsBrand_Planborgini_Should_Have_Three_Doors()
        {
         
        }

        [TestMethod]
        public void CarsSpecsBrand_Planborgini_Should_Have_Paint_With_Pink_Base()
        {
            //Arange, CarsSpecsBrand_Planborgini
            var dottedPaint = new DottedPaintJob(Color.Pink, Color.Red);

            //Act,
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.PlanfaRomeo, 5);
            painter.PaintCar(car, dottedPaint);
            var job = (DottedPaintJob)car.PaintJob;

            job.DotColor.Should().Be(Color.Pink);
        }

        [TestMethod]
        public void CarsSpecsBrand_Planborgini_Should_Have_Paint_With_Red_Dots()
        {
          
        }

        [TestMethod]
        public void CarsSpecsBrand_Planborgini_Should_Have_Paint_With_No_Stripe()
        {
        }
    }
}
