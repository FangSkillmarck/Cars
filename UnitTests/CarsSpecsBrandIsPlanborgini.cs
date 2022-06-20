using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarFactory.Controllers;
using CarFactory_Factory;
using System;
using Xunit;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Paint;
using FluentAssertions;
using System.Drawing;
using static CarFactory.Controllers.CarController;

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
//          "dotColor": "null"
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

        //        CarPaintSpecificationInputModel = new CarPaintSpecificationInputModel{
        //            type = "Planborgini",


        //    }

        //    CarSpecificationInputModel = new CarSpecificationInputModel{

        //}
        //   CarSpecification = new CarSpecification{};


        [TestMethod]
        public void CarsSpecsBrand_Planborgini_Should_Have_Quantity_15()
        {
            ////Arange, CarsSpecsBrand_Planborgini
            //var 

            ////Act,  Thursday, utctime 7:00
            //var result = FoodTimeMenuHelper.GetEarliestDeliveryTime(foodTimeCustomerConfig, now);
            //var expected = DateTimeOffset.Parse("2021-03-25T07:00:00+00:00");

            ////Assert
            //Assert.Equal(result, expected);

        }

        [TestMethod]
        public void CarsSpecsBrand_Planborgini_Should_Have_Three_Doors()
        {
            var dottedPaint = new DottedPaintJob(Color.Pink, Color.Red);
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10),"Test"), new Interior(), new Wheel[4]);
            painter.PaintCar(car, dottedPaint);
            var job = (DottedPaintJob)car.PaintJob;
            job.DotColor.Should().Be(Color.Pink);
            job.AreInstructionsUnlocked().Should().BeTrue();
        }

        [TestMethod]
        public void CarsSpecsBrand_Planborgini_Should_Have_Paint_With_Pink_Base()
        {
            //Arange, CarsSpecsBrand_Planborgini
            var dottedPaint = new DottedPaintJob(Color.Pink, Color.Red);

            //Act,
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4]);
            painter.PaintCar(car, dottedPaint);
            var job = (DottedPaintJob)car.PaintJob;

            job.DotColor.Should().Be(Color.Pink);
        }

        [TestMethod]
        public void CarsSpecsBrand_Planborgini_Should_Have_Paint_With_Red_Dots()
        {
            var singleColor = new SingleColorPaintJob(Color.Aqua);
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4]);
            painter.PaintCar(car, singleColor);
            var job = (SingleColorPaintJob)car.PaintJob;
            job.Color.Should().Be(Color.Red);
        }

        [TestMethod]
        public void CarsSpecsBrand_Planborgini_Should_Have_Paint_With_No_Stripe()
        {
            var singleColor = new SingleColorPaintJob(Color.Aqua);
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4]);
            painter.PaintCar(car, singleColor);
            var job = (SingleColorPaintJob)car.PaintJob;
            job.Color.Should().Be(Color.Aqua);
            job.AreInstructionsUnlocked().Should().BeTrue();
        }
    }
}
