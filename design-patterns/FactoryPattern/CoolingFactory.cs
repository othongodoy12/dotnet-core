namespace FactoryPattern
{
    public class CoolingFactory : AirConditionerFactory
    {
        public override IAirConditioner Create(double temperature) => 
            new CoolingManager(temperature);
    }
}