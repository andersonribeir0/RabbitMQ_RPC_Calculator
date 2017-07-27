using System;
using System.Collections.Generic;
using System.Text;

namespace RPCServer.Models
{
    public class CalculatorRequest
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Operation { get; set; }

    }
}
