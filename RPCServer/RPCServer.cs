using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RPCServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPCServer
{
    class RPCServer
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            //cria conexao para o endpoit
            using (var connection = factory.CreateConnection())
            //Abre um canal e cria uma fila
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("rpc_queue", false, false, false, null);
                channel.BasicQos(0, 1, false);
                
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume("rpc_queue", false, consumer);
                Console.WriteLine(" [x] Awaiting RPC requests");

                consumer.Received += (model, ea) =>
                {
                    string response = null;
                    //Do elemento da fila que estou lendo, preciso saber o CorrelationId desse elemento para saber para quem dar o reply.
                    var body = ea.Body;
                    var props = ea.BasicProperties;
                    var replyProps = channel.CreateBasicProperties();
                    replyProps.CorrelationId = props.CorrelationId;

                    try
                    {
                        //Lógica de negócio
                        var message = Encoding.UTF8.GetString(body);
                        var calculatorRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CalculatorRequest>(message);
                        response = Calcula(calculatorRequest.X,calculatorRequest.Y,calculatorRequest.Operador).ToString();                   
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(" [.] " + e.Message);
                        response = "";
                    }
                    finally
                    {
                        var responseBytes = Encoding.UTF8.GetBytes(response);
                        channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                          basicProperties: replyProps, body: responseBytes);

                        channel.BasicAck(deliveryTag: ea.DeliveryTag,
                          multiple: false);
                    }
                };

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
        
         //Eu sei que isso aqui não está legal, preciso refatorar. 
         private static int Calcula(int x, int y, char action)
         {
            if (action == '*')
                return new Calculator().Multiply(x, y);
            else if (action == '/')
                return new Calculator().Divide(x, y);
            else if (action == '+')
                return new Calculator().Add(x, y);
            else if (action == '-')
                return new Calculator().Substract(x, y);

            return 0;
         }

    }
}
