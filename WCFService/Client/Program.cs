using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectProcyClient.ServiceReference;
namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CalculatorServiceClient proxy =
                new CalculatorServiceClient())
            {
                try
                {
                    Console.WriteLine("x+y={2} when x={0} and y={1}", 1, 2, proxy.Add(1, 2));
                    Console.WriteLine("x/y={2} when x={0} and y={1}", 1, 0, proxy.Divide(1, 0));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                //服务异常 通道已处于Fault状态 无法使用
                //Console.WriteLine("x*y={2} when x={0} and y={1}", 2, 4, proxy.Mutiply(2, 4));
                Console.Read();
            }
        }
    }
}
