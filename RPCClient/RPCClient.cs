using RabbitMQ.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPCClient
{
    class RPCClient
    {
        private ICollection connection;
        private IModel channel;
        private string replyQueueName;
        private QueueingBasicConsumer consumer;
            
        public RPCClient()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }
        static void Main(string[] args)
        {
        }
    }
}
