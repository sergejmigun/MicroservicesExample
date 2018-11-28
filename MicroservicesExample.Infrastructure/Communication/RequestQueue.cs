using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroservicesExample.Infrastructure.Communication
{
    public class RequestQueue
    {
        private readonly int timeout = 5000;
        private readonly IModel channel;
        private readonly EventingBasicConsumer consumer;
        private readonly ConcurrentDictionary<string, Action<string>> handlers = new ConcurrentDictionary<string, Action<string>>();

        public RequestQueue(IModel channel, EventingBasicConsumer consumer)
        {
            this.channel = channel;
            this.consumer = consumer;

            consumer.Received += (sender, args) =>
            {
                if (handlers.ContainsKey(args.BasicProperties.CorrelationId))
                {
                    Action<string> handler;

                    handlers.TryGetValue(args.BasicProperties.CorrelationId, out handler);
                    handler(Encoding.UTF8.GetString(args.Body));
                }
            };
        }

        public Task<T> ConsumeResponse<T>(string messageId, byte[] messageBytes)
        {
            IBasicProperties props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = channel.QueueDeclare().QueueName;
            props.MessageId = messageId;

            var tcs = new TaskCompletionSource<T>();

            this.handlers.TryAdd(correlationId, (responseStr) =>
            {
                tcs.SetResult(JsonConvert.DeserializeObject<T>(responseStr));
            });

            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer(x => {
                Action<string> handler;

                this.handlers.TryRemove(correlationId, out handler);
                tcs.TrySetResult(default(T));
                timer.Dispose();
            }, null, this.timeout, 0);

            channel.BasicPublish(exchange: "",
                routingKey: "MicroservicesExample_RequestQueue",
                basicProperties: props,
                body: messageBytes);

            channel.BasicConsume(
                consumer: consumer,
                queue: props.ReplyTo,
                autoAck: true);

            return tcs.Task;
        }
    }
}
