using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace WebhookReceive
{
    class WebhoodReceive
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "rabbit-int.crossroads.net" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "WebhookQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                int messageNumber = 0;

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($">>>>>>>>>>>> Received message #{++messageNumber} <<<<<<<<<<<<");
                    Console.ResetColor();
                    Console.WriteLine("{0}", message);
                };
                channel.BasicConsume(queue: "WebhookQueue", autoAck: true, consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
