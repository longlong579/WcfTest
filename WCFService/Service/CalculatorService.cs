using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Interface;
using System.Threading;

namespace Service
{
    public class CalculatorService : ICalculator
    {
        public double Add(double x, double y)
        {
            return x + y;
        }
        public double Substract(double x, double y)
        {
            return x - y;
        }

        public double Divide(double x, double y)
        {
            Thread.Sleep(100);
            double n = x / y;
            return n;
        }

        public double Mutiply(double x, double y)
        {
            return x * y;
        }

        public string doSome()
        {
            return "W是另一个通道返回的信息";
        }
    }
}
