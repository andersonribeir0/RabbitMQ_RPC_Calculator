using RPCServer.Interfaces;

namespace RPCServer
{
    public class Calculator : ICalculator
    {
        public int Add(int x, int y)
        {
            
            return x + y; 
            
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
            return x - y;
        }
   
    }
}
