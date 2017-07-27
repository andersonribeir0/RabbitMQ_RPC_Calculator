using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RPCServer.Models;
using System;
using System.Text;

namespace RPCServer
{
    class Program
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "rpc_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: "rpc_queue", autoAck: false, consumer: consumer);
                Console.Write(" [x] Awaiting RPC requests...");

                consumer.Received += (model, ea) =>
                {
                    string response = null;
                    var body = ea.Body;
                    var props = ea.BasicProperties;
                    var replyProps = channel.CreateBasicProperties();
                    replyProps.CorrelationId = props.CorrelationId;

                    try
                    {
                        var message = Encoding.UTF8.GetString(body);
                        var calculatorRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CalculatorRequest>(message);
                        response = Calculate(calculatorRequest).ToString();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(" [.] " + e.Message);
                    }
                    finally
                    {
                        var responseBytes = Encoding.UTF8.GetBytes(response);
                        channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body:responseBytes);
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                };
                Console.WriteLine(" Press [ENTER] to exit.");
                Console.ReadLine();
            }
        }

        
        public static int Calculate(CalculatorRequest calculatorRequest)
        {
            switch(calculatorRequest.Operation)
            {
                case '+': return new Calculator().Multiply(calculatorRequest.X, calculatorRequest.Y);
                case '/': return new Calculator().Divide(calculatorRequest.X, calculatorRequest.Y);
                case '*': return new Calculator().Multiply(calculatorRequest.X, calculatorRequest.Y);
                case '-': return new Calculator().Substract(calculatorRequest.X, calculatorRequest.Y);
                default: throw new Exception("Invalid operator!");
            }
        }
    }
}