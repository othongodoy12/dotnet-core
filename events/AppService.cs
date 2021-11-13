using System;

namespace events
{
    public class AppService
    {
        public void OnFoodPrepared(object source, FoodPreparedEventArgs eventArgs)
        {
            Console.WriteLine($"AppService: your food '{eventArgs.Order.Item}' is prepared.");
        }
    }
}