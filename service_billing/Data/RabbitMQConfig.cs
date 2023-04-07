using System;
using RabbitMQ.Client;

namespace service_billing.Data
{
	public class RabbitMQConfig
	{
        public static ConnectionFactory GetConnectionFactory()
        {
            return new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                DispatchConsumersAsync = true
            };
        }
    }
}

