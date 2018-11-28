using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroservicesExample.Orders.Infrastructure
{
    public static class MessageQueue
    {
        public static async Task<T> SendMessageWithResponse<T>(string messageName, object data)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            IConnection connection = factory.CreateConnection();
            IModel model = connection.CreateModel();
            string replyQueueName = model.QueueDeclare().QueueName;
            EventingBasicConsumer consumer = new EventingBasicConsumer(model);


            IBasicProperties props = model.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            props.MessageId


            var tcs = new TaskCompletionSource<T>();

            consumer.Received += (sender, args) =>
            {
                if (args.BasicProperties.CorrelationId == correlationId)
                {
                    tcs.SetResult(JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(args.Body)));
                }
            };

            var messageBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));

            model.BasicPublish(
                exchange: "",
                routingKey: "rpc_queue",
                basicProperties: props,
                body: messageBytes);

            model.BasicConsume(
                consumer: consumer,
                queue: replyQueueName,
                autoAck: true);


            return await tcs.Task;
        }
    }
}
