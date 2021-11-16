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

            using (var connection = factory.CreateConnection())
            {
                var queueName = "order";

                var channel1 = CreateChannel(connection);
                var channel2 = CreateChannel(connection);

                BuildPublishers(channel1, queueName, "Produtor A");
                BuildPublishers(channel2, queueName, "Produtor B");

                Console.ReadLine();
            }
        }

        private static IModel CreateChannel(IConnection connection) => connection.CreateModel();

        private static void BuildPublishers(IModel channel, string queueName, string publisherName)
        {
            Task.Run(() =>             
            {
                var count = 0;

                channel.QueueDeclare(
                    queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                while(true)
                {
                    var message = $"OrderNumber: {count++} from {publisherName}";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", queueName, null, body);

                    Console.WriteLine($"{publisherName}: [x] Sent {count}", message);

                    Thread.Sleep(1000);
                }
            });
        }
    }
}
