using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Paint;
using FluentAssertions;
using System.Drawing;
using static CarFactory.Controllers.CarController;

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
//        "manufacturer": "Planborgini",
//        "frontWindowSpeakers": [
//          {
//            "isSubwoofer": true
//          }
//        ]
//      }
//    }
//  ]
//}
// |Planborgini|3 doors|Pink base with red dots|10 subwoofers and 20 standard|15| 

namespace UnitTests
{
    [TestClass]
    public class CarsSpecsBrandIsPlanborgini
    {
        //Arange, CarsSpecsBrand_Planborghini
        public static CarPaintSpecificationInputModel specPaint = new CarPaintSpecificationInputModel
        {
            Type = "Dot ",
            BaseColor = "Pink",
            StripeColor = null,
            DotColor = "Red"
        };

        public static CarSpecificationInputModel inputModel = new CarSpecificationInputModel
        {
            NumberOfDoors = 3,
            Paint = specPaint,
            Manufacturer = Manufacturer.Planborgini
        };

        public static BuildCarInputModelItem carSpecItem = new BuildCarInputModelItem
        {
            Amount = 15,
            Specification = inputModel
        };


        [TestMethod]
        public void PaintType_Dot_With_Capital_Letter_Should_Work_As_Small_lettter_dot()
        {
            var paintType = specPaint.Type;
            var painterJob = new DottedPaintJob(ParseColor(specPaint.BaseColor), ParseColor(specPaint.DotColor));
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.Planborgini, 3);
            painter.PaintCar(car, painterJob);
            var job = (DottedPaintJob)car.PaintJob;
            job.Should().NotBeNull();
        }


        [TestMethod]
        public void CarsSpecsBrand_Planborghini_Should_Have_Quantity_15()
        {
            var amountSpec = carSpecItem.Amount;
            var car = new Car[15];
            for (var i = 0; i < amountSpec; i++)
                car[i] = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.Planborgini, 3);

            Assert.AreEqual(car.Length, amountSpec);
        }

        [TestMethod]
        public void CarsSpecsBrand_Planborghini_Should_Have_Three_Doors()
        {
            var doorNumbers = inputModel.NumberOfDoors;
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.Planborgini, 3);

            Assert.AreEqual(doorNumbers, car.NumberOfDoors);
        }


        [TestMethod]
        public void CarsSpecsBrand_Planborghini_Should_Have_Paint_With_Pink_Base()
        {
            var painterJob = new DottedPaintJob(ParseColor(specPaint.BaseColor), ParseColor(specPaint.DotColor));
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.Planborgini, 3);
            painter.PaintCar(car, painterJob);
            var job = (DottedPaintJob)car.PaintJob;
            job.BaseColor.Should().Be(Color.Pink);
        }

        [TestMethod]
        public void CarsSpecsBrand_Planborghini_Should_Have_Paint_With_Red_Dot()
        {
            var painterJob = new DottedPaintJob(ParseColor(specPaint.BaseColor), ParseColor(specPaint.DotColor));
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.Planborgini, 3);
            painter.PaintCar(car, painterJob);
            var job = (DottedPaintJob)car.PaintJob;
            job.DotColor.Should().Be(Color.Red);
        }

      
    }
}
