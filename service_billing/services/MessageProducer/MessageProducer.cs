using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace service_billing.services.MessageProducer
{
	public class MessageProducer : IMessageProducer
	{
		public MessageProducer()
		{
		}

        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };

            var conn = factory.CreateConnection();

            using var channel = conn.CreateModel();

            channel.QueueDeclare(queue: "transactions",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);


            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", "transactions", body: body);
        }
    }
}

