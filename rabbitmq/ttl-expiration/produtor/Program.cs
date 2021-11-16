using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace produtor
{
    class Program
    {
        static void Main()
        {
            var queueName = "test_time_to_alive";

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // channel.QueueDeclare(
                //     queue: queueName,
                //     durable: false,
                //     exclusive: false,
                //     autoDelete: false,
                //     arguments: null
                // );

                // var body = Encoding.UTF8.GetBytes($"Hello World! Data/Hora: {DateTime.Now}");

                // var props = channel.CreateBasicProperties();

                // props.Expiration = "5000";

                // channel.BasicPublish(
                //     exchange: "",
                //     routingKey: queueName,
                //     basicProperties: props,
                //     body: body
                // );

                var args = new Dictionary<string, object>();

                args.Add("x-message-ttl", 20000);

                channel.QueueDeclare(
                    queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: args
                );

                var body = Encoding.UTF8.GetBytes($"Hello World! Data/Hora: {DateTime.Now}");               

                channel.BasicPublish(
                    exchange: "",
                    routingKey: queueName,
                    basicProperties: null,
                    body: body
                );
            }

            Console.WriteLine("Press [enter] to exit.");

            Console.ReadLine();
        }
    }
}
