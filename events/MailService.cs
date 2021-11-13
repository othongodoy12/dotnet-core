using System;

namespace events
{
    public class MailService
    {
        public void OnFoodPrepared(object source, FoodPreparedEventArgs eventArgs)
        {
            Console.WriteLine($"MailService: your food '{eventArgs.Order.Item}' is prepared.");
        }
    }
}