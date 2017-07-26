using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using RPCServer.Model;

    public class RPCClient
    {
        private IConnection connection;
        private IModel channel;
        private string replyQueueName;
        private QueueingBasicConsumer consumer;
        //private EventingBasicConsumer consumer2;

        public RPCClient()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            //Fila de callback (resposta)
            Console.WriteLine("replyQueueName " + replyQueueName);
            consumer = new QueueingBasicConsumer(channel);
            //consumer2 = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: replyQueueName, autoAck: true, consumer: consumer);
        }

        public string Call(string message)
        {
            var corrId = Guid.NewGuid().ToString();
            var props = channel.CreateBasicProperties();
            
            props.ReplyTo = replyQueueName;
            props.CorrelationId = corrId;            

            var messageBytes = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "", routingKey: "rpc_queue", basicProperties: props, body: messageBytes);

          
            while (true)
            {
                var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                                
                if (ea.BasicProperties.CorrelationId == corrId)
                {
                    return Encoding.UTF8.GetString(ea.Body);
                }
            }
        }

        public void Close()
        {
            connection.Close();
        }
    }

}
