using System;
namespace service_billing.services.MessageProducer
{
	public interface IMessageProducer
	{
		public void SendMessage<T>(T message);
	}
}

