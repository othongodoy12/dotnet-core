namespace FactoryPattern
{
    public class WarmingFactory : AirConditionerFactory
    {
        public override IAirConditioner Create(double temperature) => 
            new WarmingManager(temperature);
    }
}