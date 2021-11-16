﻿using System;
using System.Text;
using System.Threading;
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
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "hello",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                int count = 0;

                while (true)
                {
                    string message = $"{count++} hello world";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "hello",
                        basicProperties: null,
                        body: body
                    );

                    Console.WriteLine("[x] Sent {0}", message);

                    Thread.Sleep(200);
                }
            }

            Console.WriteLine("Press [enter] to exit.");

            Console.ReadLine();
        }
    }
}
