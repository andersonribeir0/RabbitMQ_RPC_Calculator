using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPCServer.Interface
{
    interface ICalculator
    {
        int Add(int x, int y);
        int Substract(int x, int y);
        int Divide(int x, int y);
        int Multiply(int x, int y);
    }
}
