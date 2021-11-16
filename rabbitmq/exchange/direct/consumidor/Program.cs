using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace consumidor
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
                var channel = connection.CreateModel();

                channel.QueueDeclare(
                    queue: "order",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                BuildAndRunWorker(channel, "Worker 1");
                BuildAndRunWorker(channel, "Worker 2");

                Console.WriteLine("Press [enter] to exit.");

                Console.ReadLine();
            }
        }        

        private static void BuildAndRunWorker(IModel channel, string workerName)
        {
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"{workerName}: [x] Received {message}");
            };

            channel.BasicConsume(
                queue: "order",
                autoAck: true,
                consumer: consumer
            );            
        }
    }
}
