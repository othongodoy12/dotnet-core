using System;
using System.Text;
using RabbitMQ.Client;

namespace produtor
{
    class Program
    {
        static void Main()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "durable_queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var body = Encoding.UTF8.GetBytes($"Hello World! Data/Hora: {DateTime.Now}");   

                var basicProp = channel.CreateBasicProperties();

                basicProp.Persistent = true;

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "durable_queue",
                    basicProperties: basicProp,
                    body: body
                );

                Console.WriteLine("[x] Sent {0}", body);
            }

            Console.WriteLine("Press [enter] to exit.");

            Console.ReadLine();
        }
    }
}
