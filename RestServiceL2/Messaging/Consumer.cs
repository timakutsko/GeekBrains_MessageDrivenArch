using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Messaging
{
    /// <summary>
    /// Реализация использование Topic: https://www.rabbitmq.com/tutorials/tutorial-five-dotnet.html
    /// </summary>
    public class Consumer : IDisposable
    {
        private readonly string _hostName;
        private readonly string _routingKey;

        private readonly IConnection _connection;
        private readonly IModel _channel;

        public Consumer(string hostName, string routingKey)
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

        public void Receive(EventHandler<BasicDeliverEventArgs> receiveCallBack)
        {
            // Объявляем обменник
            _channel.ExchangeDeclare("topic_exchange", ExchangeType.Topic);

            // Объявляем очередь
            var queueName = _channel.QueueDeclare().QueueName;

            // Биндим
            _channel.QueueBind(queueName, "topic_exchange", _routingKey);

            // Добовляем обработчик события приема сообщения
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += receiveCallBack;

            _channel.BasicConsume(queueName, true, consumer);
        }
    }
}
