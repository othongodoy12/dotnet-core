using System;

namespace CompositePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // single gift
            var phone = new SingleGift("Phone", 256);

            phone.CalculateTotalPrice();

            // \n
            Console.WriteLine();

            //composite gift
            var rootBox = new CompositeGift("RootBox");

            var truckToy = new SingleGift("TruckToy", 289);
            var plainToy = new SingleGift("PlainToy", 587);

            rootBox.Add(truckToy);
            rootBox.Add(plainToy);

            var childBox = new CompositeGift("ChildBox");
            var soldierToy = new SingleGift("SoldierToy", 200);

            childBox.Add(soldierToy);
            
            rootBox.Add(childBox);

            Console.WriteLine($"Total price of this composite present is: {rootBox.CalculateTotalPrice()}");
        }
    }
}
