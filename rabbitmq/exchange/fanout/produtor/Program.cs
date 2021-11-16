using System;
using System.Text;
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
                queue: "logs",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            channel.QueueDeclare(
                queue: "finance_orders",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            channel.ExchangeDeclare("order", "fanout");

            channel.QueueBind("order", "order", "");
            channel.QueueBind("logs", "order", "");
            channel.QueueBind("finance_orders", "order", "");

            return channel;
        } 
            

        private static void BuildPublishers(IModel channel, string queueName, string publisherName, ManualResetEvent manualResetEvent)
        {
            Task.Run(() =>             
            {
                var count = 0;

                while(true)
                {
                    try
                    {
                        Console.WriteLine("Pressione [enter] para produzir 10 msgs");
                        Console.ReadLine();

                        for(var i = 0; i < 10; i++)
                        {
                            var message = $"OrderNumber: {count++} from {publisherName}";

                            var body = Encoding.UTF8.GetBytes(message);

                            channel.BasicPublish("order", "", null, body);

                            Console.WriteLine($"{publisherName}: [x] Sent {count}", message);
                        }
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
