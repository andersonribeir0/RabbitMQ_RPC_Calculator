using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPCServer.Model
{
    public class CalculatorRequest
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Operador { get; set; }

    }
}
