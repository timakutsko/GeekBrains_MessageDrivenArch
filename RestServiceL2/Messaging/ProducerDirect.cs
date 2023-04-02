using RabbitMQ.Client;
using System;
using System.Text;

namespace Messaging
{
    public class ProducerDirect : IDisposable
    {
        private readonly string _queueName;
        private readonly string _hostName;

        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ProducerDirect(string queueName, string hostName)
        {
            _queueName = queueName;
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

        #region "Work Queue"
        //public void Send(string message, string exchangeType)
        //{
        //    _channel.ExchangeDeclare("direct_exchange", exchangeType, false, false, null);

        //    byte[] body = Encoding.UTF8.GetBytes(message);

        //    _channel.BasicPublish("direct_exchange", _queueName, null, body);
        //}
        #endregion

        public void Send(string message, string exchangeType)
        {
            _channel.ExchangeDeclare("direct_exchange", exchangeType, false, false, null);

            byte[] body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish("direct_exchange", _queueName, null, body);
        }
    }
}
