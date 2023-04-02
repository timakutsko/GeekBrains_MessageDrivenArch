using RabbitMQ.Client;
using System;
using System.Text;

namespace Messaging
{
    public class Producer : IDisposable
    {
        private readonly string _hostName;
        private readonly string _routingKey;

        private readonly IConnection _connection;
        private readonly IModel _channel;

        public Producer(string hostName, string routingKey)
        {
            _hostName = hostName;
            _routingKey = routingKey;

            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = _hostName,
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }

        public void Send(string message, string exchangeType)
        {
            _channel.ExchangeDeclare("topic_exchange", exchangeType, false, false, null);

            byte[] body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish("topic_exchange", _routingKey, null, body);
        }
    }
}
