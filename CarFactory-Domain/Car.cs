using System;
using System.Collections.Generic;

namespace CarFactory_Domain
{
    public class Car
    {
        public Chassis Chassis { get; set; }
        public Engine.Engine Engine { get; }
        public PaintJob PaintJob { get; set; }
        public Interior Interior { get; set; }
        public IEnumerable<Wheel> Wheels { get; set; }
        public Manufacturer Manufacturer{ get; }
        public long CarLockSetting { get; private set; }
        public int NumberOfDoors { get; set; }
        //public CarPaintSpecificationInputModel paintSpec
        public Car(Chassis chassis, Engine.Engine engine, Interior interior, IEnumerable<Wheel> wheels, Manufacturer manufacturer, int numberOfDoors)
        {
            Chassis = chassis ?? throw new ArgumentNullException(nameof(chassis));
            Engine = engine ?? throw new ArgumentNullException(nameof(engine));
            Interior = interior ?? throw new ArgumentNullException(nameof(interior));
            Wheels = wheels ?? throw new ArgumentNullException(nameof(wheels));
            Manufacturer = Manufacturer;
            NumberOfDoors = numberOfDoors;
        }

        public void SetCarLockSettings(long setting)
        {
            CarLockSetting = setting;
        }
    }
}
