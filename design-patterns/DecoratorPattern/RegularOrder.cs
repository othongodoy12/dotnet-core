using System;
using System.Linq;

namespace DecoratorPattern
{
    public class RegularOrder : OrderBase
    {
        public override double CalculateTotalOrderPrice()
        {
            Console.WriteLine("Calculating the total price of a regular order");
            
            return products.Sum(x => x.Price);
        }
    }
}