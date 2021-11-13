using System;
using System.Threading;

namespace events
{
    public class FoodOrderingService
    {
        // // define a delegate
        // public delegate void FoodPreparedEventHandler(object source, FoodPreparedEventArgs args);

        // // declare the event
        // public event FoodPreparedEventHandler FoodPrepared;

        public event EventHandler<FoodPreparedEventArgs> FoodPrepared;

        public void PrepareOrder(Order order)
        {
            Console.WriteLine($"Preparing your order '{order.Item}', please wait...");
            Thread.Sleep(4000);

            OnFoodPrepared(order);
        }

        protected virtual void OnFoodPrepared(Order order)
        {
            // if (FoodPrepared != null)
            //     FoodPrepared(this, new FoodPreparedEventArgs { Order = order });

            FoodPrepared?.Invoke(this, new FoodPreparedEventArgs { Order = order });
        }
    }
}