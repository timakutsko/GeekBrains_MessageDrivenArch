using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Messaging
{
    /// <summary>
    /// Реализация паттерна "Work Queue": https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html
    /// </summary>
    public class ConsumerDirect : IDisposable
    {
        private readonly string _queueName;
        private readonly string _hostName;

        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ConsumerDirect(string queueName, string hostName)
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

        public void Receive(EventHandler<BasicDeliverEventArgs> receiveCallBack)
        {
            // Объявляем обменник
            _channel.ExchangeDeclare("direct_exchange", ExchangeType.Direct);

            // Объявляем очередь
            _channel.QueueDeclare(_queueName, false, false, false, null);

            // Биндим
            _channel.QueueBind(_queueName, "direct_exchange", _queueName);

            // Добовляем обработчик события приема сообщения
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += receiveCallBack;

            _channel.BasicConsume(_queueName, true, consumer);
        }
    }
}
