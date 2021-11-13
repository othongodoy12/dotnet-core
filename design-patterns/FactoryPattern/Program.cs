using System;

namespace FactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new AirConditioner().ExecuteCreation(Actions.Cooling, 22.5);
            
            factory.Operate();
        }
    }
}
