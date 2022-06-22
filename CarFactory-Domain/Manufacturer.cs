namespace CarFactory_Domain
{
    public enum Manufacturer
    {
        Planborghini = 1,
        PlandayMotorWorks = 2,
        PlanfaRomeo = 3,
        AstonPlanday = 4,
        Plandrover = 5,
        Volksday = 6
    }

    public class Manufacture
    {
        public Manufacturer Manufacturer { get; set; }
        public Manufacture(Manufacturer manufacture)
        {
          Manufacturer = manufacture;
        }
    
    }

}