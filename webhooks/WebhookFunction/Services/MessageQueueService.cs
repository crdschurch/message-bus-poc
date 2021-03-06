﻿using System.Text;
using RabbitMQ.Client;

namespace AzureFunctionSample.Services
{
    public class MessageQueueService : IMessageQueueService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly string QUEUE_NAME = "WebhookQueue";

        public MessageQueueService()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
//                UserName = user,
//                Password = pass,
//                VirtualHost = vhost,
                HostName = "104.211.5.25",
                Port = 5672
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: QUEUE_NAME,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }

        public bool AddMessageToQueue(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var body = Encoding.UTF8.GetBytes(message);

                lock (_channel)
                {
                    _channel.BasicPublish(exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: body);
                }

                return true;
            }

            return false;
        }
    }
}
