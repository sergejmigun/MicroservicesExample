using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroservicesExample.Infrastructure.Communication
{
    public static class MessageQueue
    {
        static RequestQueue requestQueue;

        public static void Init(IMediator mediator)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            InitRequestsChannel(connection, mediator);
            InitResponsesChannel(connection);
        }

        public static Task<T> SendMessage<T>(string command, object requestObj)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestObj));

            return requestQueue.ConsumeResponse<T>(command, messageBytes);
        }

        private static void InitRequestsChannel(IConnection connection, IMediator mediator)
        {
            IModel channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            channel.QueueDeclare(queue: "MicroservicesExample_RequestQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(0, 1, false);
            channel.BasicConsume(queue: "MicroservicesExample_RequestQueue", autoAck: false, consumer: consumer);

            consumer.Received += (sender, args) =>
            {
                string commandName = args.BasicProperties.MessageId;
                var types = Assembly.GetEntryAssembly().GetTypes();

                Type commandType = Assembly.GetEntryAssembly().GetTypes().Where(x => x.Name == $"{commandName}Command").FirstOrDefault();

                string response = null;

                if (commandType != null)
                {
                    string message = Encoding.UTF8.GetString(args.Body);
                    dynamic command = JsonConvert.DeserializeObject(message, commandType);
                    var ttt = command.GetType();
                    dynamic task = mediator.Send(command);

                    response = JsonConvert.SerializeObject(task.Result);

                    var responseBytes = Encoding.UTF8.GetBytes(response);

                    var replyProps = channel.CreateBasicProperties();
                    replyProps.CorrelationId = args.BasicProperties.CorrelationId;

                    channel.BasicPublish(exchange: "", routingKey: args.BasicProperties.ReplyTo, basicProperties: replyProps, body: responseBytes);
                    channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
                }
            };
        }

        private static void InitResponsesChannel(IConnection connection)
        {
            IModel channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            channel.QueueDeclare(queue: "MicroservicesExample_ResponseQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(0, 1, false);
            channel.BasicConsume(queue: "MicroservicesExample_ResponseQueue", autoAck: false, consumer: consumer);

            requestQueue = new RequestQueue(channel, consumer);
        }
    }
}
