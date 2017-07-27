using System;
using System.Collections.Generic;
using System.Text;

namespace RPCServer.Interfaces
{
    public interface ICalculator
    {
        int Add(int x, int y);
        int Substract(int x, int y);
        int Multiply(int x, int y);
        int Divide(int x, int y);
    }
}
