﻿using System;

namespace events
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = new Order { Item = "Pizza with extra cheese" };

            var orderingService = new FoodOrderingService();

            var appService = new AppService();
            var mailService = new MailService();

            orderingService.FoodPrepared += appService.OnFoodPrepared;
            orderingService.FoodPrepared += mailService.OnFoodPrepared;

            // to unsubscribe
            // orderingService.FoodPrepared -= appService.OnFoodPrepared;
            // orderingService.FoodPrepared -= mailService.OnFoodPrepared;

            orderingService.PrepareOrder(order);

            Console.ReadKey();
        }
    }
}
