using CarFactory.Controllers;

using CarFactory_Factory;

using System;

using Xunit;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Paint;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;


using static CarFactory.Controllers.CarController;

namespace CarFactoryAPI.Test
{
    public class CarControllerTest
    {
        CarController _carController;
        

        private readonly ICarFactory _carFactory;


        public CarControllerTest(ICarFactory carFactory)
        {
            _carFactory = carFactory;
        }

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
        [Fact] 
        public void AddCarSpecTest()
        {

            //OK RESULT TEST START

            //Arrange
            var completeCars = new BuildCarInputModel(); 
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
}
