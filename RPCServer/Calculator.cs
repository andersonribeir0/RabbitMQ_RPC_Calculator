using RPCServer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPCServer
{
    class Calculator : ICalculator
    {
        public int Add(int x, int y)
        {
            return x+y;
        }

        public int Divide(int x, int y)
        {
            return x / y;
        }

        public int Multiply(int x, int y)
        {
            return x * y;
        }

        public int Substract(int x, int y)
        {
            return x-y;
        }
    }
}
