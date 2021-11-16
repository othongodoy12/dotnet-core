using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace worker
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
                    queue: "order",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        var order = JsonSerializer.Deserialize<Order>(message);

                        channel.BasicAck(ea.DeliveryTag, false);

                        Console.WriteLine($"[x] Received Order Id {order.Id} | {order.ItemName} | {order.Price:N2}");
                    }
                    catch
                    {
                        channel.BasicNack(ea.DeliveryTag, false, true);
                    }
                };

                channel.BasicConsume(
                    queue: "order",
                    autoAck: false,
                    consumer: consumer
                );

                Console.ReadLine();
            }
        }
    }
}
