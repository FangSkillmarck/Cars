using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Paint;
using FluentAssertions;
using System.Drawing;
using static CarFactory.Controllers.CarController;

namespace UnitTests
{
    [TestClass]
    public class CarsSpecsBrandIsPlanfaRomeo
    {
        //Arange, CarsSpecsBrand_PlanfaRomeo
        public static CarPaintSpecificationInputModel specPaint = new CarPaintSpecificationInputModel
        {
            Type = "Stripe ",
            BaseColor = "Blue",
            StripeColor = "Orange",
            DotColor = null
        };

        public static CarSpecificationInputModel inputModel = new CarSpecificationInputModel
        {
            NumberOfDoors = 5,
            Paint = specPaint,
            Manufacturer = Manufacturer.Planborgini
        };

        public static BuildCarInputModelItem carSpecItem = new BuildCarInputModelItem
        {
            Amount = 75,
            Specification = inputModel
        };

        [TestMethod]
        public void PaintType_Stripe_With_Capital_Letter_Should_Work_As_Small_lettter_Stripe()
        {
            var paintType = specPaint.Type;
            var painterJob = new StripedPaintJob(ParseColor(specPaint.BaseColor), ParseColor(specPaint.StripeColor));
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.PlanfaRomeo, 5);
            painter.PaintCar(car, painterJob);
            var job = (StripedPaintJob)car.PaintJob;
            job.Should().NotBeNull();
        }

        [TestMethod]
        public void CarsSpecsBrand_PlanfaRomeo_Should_Have_Quantity_75()
        {
            var amountSpec = carSpecItem.Amount;
            var car = new Car[75];
            for (var i = 0; i <amountSpec; i++)
                car[i] = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.PlanfaRomeo, 5);
        
            Assert.AreEqual(car.Length, amountSpec);
        }

        [TestMethod]
        public void CarsSpecsBrand_PlanfaRomeo_Should_Have_Five_Doors()
        {
            var doorNumbers = inputModel.NumberOfDoors;
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.PlanfaRomeo, 5);

            Assert.AreEqual(doorNumbers, car.NumberOfDoors);
        }

        [TestMethod]
        public void CarsSpecsBrand_PlanfaRomeo_Should_Have_Paint_With_Blue_Base()
        {
            var painterJob = new StripedPaintJob(ParseColor(specPaint.BaseColor), ParseColor(specPaint.StripeColor));
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.PlanfaRomeo, 5);
            painter.PaintCar(car, painterJob);
            var job = (StripedPaintJob)car.PaintJob;
            job.BaseColor.Should().Be(Color.Blue);
        }

        [TestMethod]
        public void CarsSpecsBrand_PlanfaRomeo_Should_Have_Paint_With_Orange_Stripes()
        {
        var painterJob = new StripedPaintJob(ParseColor(specPaint.BaseColor), ParseColor(specPaint.StripeColor));
        var painter = new Painter();
        var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4], Manufacturer.PlanfaRomeo, 5);
        painter.PaintCar(car, painterJob);
        var job = (StripedPaintJob)car.PaintJob;
        job.StripeColor.Should().Be(Color.Orange);
         }
    }
}
