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
            while (true)
            {
                Console.WriteLine("请选则测试类型： 1:测试普通服务异常处理，2:测试ErrorHandler处理");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    TestNormalError();
                }
                else if (input == "2")
                {
                    TestErrorHandler();
                }
            }
  
        }

        private static void TestErrorHandler()
        {
            using (ChannelFactory<IErrorHandleTest> channelFactory = new ChannelFactory<IErrorHandleTest>("Tcp_ErrorHandleService"))
            {
                IErrorHandleTest proxy = null;
                try
                {
                    proxy = channelFactory.CreateChannel();
                    (proxy as ICommunicationObject).Closed += delegate
                    {
                        Console.WriteLine("服务代理关闭成功！");
                    };
                    Console.WriteLine("x/y={2} when x={0} and y={1}", 1, 2, proxy.DivideInt(1, 0));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    try
                    {
                        Console.WriteLine("x/y={2} when x={0} and y={1}", 4, 2, proxy.DivideInt(4, 2));
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(channelFactory.State.ToString() + "  " + ((ICommunicationObject)proxy).State.ToString());
                        Console.WriteLine(ee.ToString());
                    }
                }
            }
        }

        private static void TestNormalError()
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
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    try
                    {
                        Console.WriteLine("x+y={2} when x={0} and y={1}", 1, 2, proxy.Add(1, 2));
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(channelFactory.State.ToString() + "  " + ((ICommunicationObject)proxy).State.ToString());
                        Console.WriteLine(ee.ToString());
                    }
                }
            }
        }
    }
}
