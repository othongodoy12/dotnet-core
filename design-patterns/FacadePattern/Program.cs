using System;
using System.Collections.Generic;

namespace FacadePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var restaurant = new OnlineRestaurant();

            var shippingService = new ShippingService();

            var facade = new Facade(restaurant, shippingService);

            var chickenOrder = new Order() { DishName = "Chicken with rice", DishPrice = 20.0, User = "User1", ShippingAddress = "Random Street 123" };            
            var sushiOrder = new Order() { DishName = "Sushi", DishPrice = 52.0, User = "User2", ShippingAddress = "More Random Street 321" };
            
            facade.OrderFood(new List<Order>() { chickenOrder, sushiOrder });
            
            Console.ReadLine();
        }
    }
}
