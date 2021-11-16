using System;
using System.Collections.Generic;
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
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("DeadLetterExchange", ExchangeType.Fanout);

                channel.QueueDeclare(
                    queue: "dead_letter_queue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                channel.QueueBind("dead_letter_queue", "DeadLetterExchange", "");  

                var arguments = new Dictionary<string, object>()
                {
                    { "x-dead-letter-exchange", "DeadLetterExchange" }
                };

                channel.QueueDeclare(
                    queue: "task_queue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: arguments
                );                              

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                Console.WriteLine("[*] Waiting for messages...");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        var num = int.Parse(message);

                        Console.WriteLine("[x] Received {0}", num);

                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                        channel.BasicNack(ea.DeliveryTag, false, false);
                    }
                };

                channel.BasicConsume(
                    queue: "task_queue",
                    autoAck: false,
                    consumer: consumer
                );

                Console.WriteLine("Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
