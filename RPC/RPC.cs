using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RPCServer.Model;

namespace RPC
{
    class RPC
    {
        static void Main(string[] args)
        {
            var rpcClient = new RPCClient();
            var calculator = new CalculatorRequest()
            {
                X = 6,
                Y = 2,
                Operador = '*'
            };
            Console.WriteLine(" [x] Requesting calc(json_calculator)");
            var response = rpcClient.Call((Newtonsoft.Json.JsonConvert.SerializeObject(calculator)));
            Console.WriteLine(" [.] Got '{0}'", response);
            //Console.WriteLine(" [x] Requesting fib(30)");
            //var response = rpcClient.Call("30");
            //Console.WriteLine(" [.] Got '{0}'", response);

            rpcClient.Close();
            Console.ReadKey();
        }
    }
}
