using Artech.BatchingHosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("Tcp1_CalculatorService"))
            {
                ICalculator proxy = null;
                try
                {
                    proxy = channelFactory.CreateChannel();
                    (proxy as ICommunicationObject).Closed += delegate
                    {
                        Console.WriteLine("服务代理关闭成功！");
                    };
                    Console.WriteLine("x+y={2} when x={0} and y={1}", 1, 2, proxy.Add(1, 2));
                    Console.WriteLine("x/y={2} when x={0} and y={1}", 1, 0, proxy.DivideInt(1, 0));
                    Console.ReadKey();
                }
                catch(Exception e)
                {
                   // Console.WriteLine(e.ToString());
                    try
                    {
                        Console.WriteLine("x+y={2} when x={0} and y={1}", 1, 2, proxy.Add(1, 2));
                    }
                    catch(Exception ee)
                    {
                        Console.WriteLine(channelFactory.State.ToString()+"  "+((ICommunicationObject)proxy).State.ToString());
                        Console.WriteLine(ee.ToString());
                        Console.ReadKey();
                    }
                    Console.ReadKey();
                }
             }
        }
    }
}
