using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace produtor
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            var manualResetEvent = new ManualResetEvent(false);
            manualResetEvent.Reset();

            using (var connection = factory.CreateConnection())
            {
                var queueName = "order";

                var channel1 = SetupChannel(connection);

                BuildPublishers(channel1, queueName, "Produtor A", manualResetEvent);

                manualResetEvent.WaitOne();
            }
        }

        private static IModel SetupChannel(IConnection connection) 
        {
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "order",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            channel.QueueDeclare(
                queue: "finance_order",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            channel.ExchangeDeclare("order", "direct");

            channel.QueueBind("order", "order", "order_new");
            channel.QueueBind("order", "order", "order_upd");
            channel.QueueBind("finance_order", "order", "order_new");

            return channel;
        } 
            

        private static void BuildPublishers(IModel channel, string queueName, string publisherName, ManualResetEvent manualResetEvent)
        {
            Task.Run(() =>             
            {
                var id = 1;

                var random = new Random(DateTime.UtcNow.Millisecond * DateTime.UtcNow.Second);

                while(true)
                {
                    try
                    {
                        Console.WriteLine("Pressione [enter] para produzir uma msg");
                        Console.ReadLine();

                        var order = new Order(id++, random.Next(1000, 9999));
                        var message1 = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(order));

                        channel.BasicPublish("order", "order_new", null, message1);

                        Console.WriteLine($"New Order Id {order.Id}: Amount {order.Amount} | Created At: {order.CreatedAt:o}");

                        order.UpdateOrder(random.Next(1000, 9999));
                        var message2 = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(order));

                        channel.BasicPublish("order", "order_upd", null, message2);

                        Console.WriteLine($"Upd Order Id {order.Id}: Amount {order.Amount} | Last Update At: {order.LastUpdated:o}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                        manualResetEvent.Set();
                    }
                }                
            });
        }
    }
}
