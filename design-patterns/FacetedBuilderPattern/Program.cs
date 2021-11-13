using System;

namespace FacetedBuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var car = new CarBuilderFacade()
                .Info
                    .WithType("BMW")
                    .WithColor("Black")
                    .WithNumberOfDoors(2)
                .Built
                    .InCity("Leipzig ")
                    .AtAddress("Some address 254")
                .Build();

            Console.WriteLine(car);
        }
    }
}
