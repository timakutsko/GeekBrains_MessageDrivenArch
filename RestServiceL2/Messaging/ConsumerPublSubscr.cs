using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Messaging
{
    /// <summary>
    /// Реализация паттерна "publish/subscribe": https://www.rabbitmq.com/tutorials/tutorial-three-dotnet.html
    /// </summary>
    public class ConsumerPublSubscr : IDisposable
    {
        private readonly string _hostName;

        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ConsumerPublSubscr(string hostName)
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

        public void Receive(EventHandler<BasicDeliverEventArgs> receiveCallBack)
        {
            // Объявляем обменник
            _channel.ExchangeDeclare("fanout_exchange", ExchangeType.Fanout);

            // Объявляем очередь
            var queueName = _channel.QueueDeclare().QueueName;

            // Биндим
            _channel.QueueBind(queueName, "fanout_exchange", queueName);

            // Добовляем обработчик события приема сообщения
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += receiveCallBack;

            _channel.BasicConsume(queueName, true, consumer);
        }
    }
}
