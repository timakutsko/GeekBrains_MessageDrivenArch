using RabbitMQ.Client;
using System;
using System.Text;

namespace Messaging
{
    public class ProducerPublSubscr : IDisposable
    {
        private readonly string _hostName;

        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ProducerPublSubscr(string hostName)
        {
            _hostName = hostName;

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
            _channel.ExchangeDeclare("fanout_exchange", exchangeType, false, false, null);

            byte[] body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish("fanout_exchange", String.Empty, null, body);
        }
    }
}
