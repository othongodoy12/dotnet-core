using System;

namespace events
{
    public class FoodPreparedEventArgs : EventArgs
    {
        public Order Order { get; set; }
    }
}