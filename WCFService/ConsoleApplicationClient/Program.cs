using Artech.BatchingHosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.XHL;

namespace ConsoleApplicationClient
{
    class Program
    {
        static void Main(string[] args)
        {         
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("请选则测试类型：1:服务异常未处理,关闭通道。 2:测试普通服务异常处理。 3:测试ErrorHandler处理。");
                Console.WriteLine("4:创建通道调用服务并自动关闭。  5:会话限流测试。");
                Console.WriteLine();
 
                string input = Console.ReadLine();
                if (input == "1")
                {
                    TestErrorNotDeal();
                }
                else if (input == "2")
                {
                    TestNormalError();
                }
                else if (input == "3")
                {
                    TestErrorHandler();
                }
                else if (input == "4")
                {
                    TestAutoAbortChannelWhenError();
                }
                else if (input == "5")
                {
                    TestLimit();
                }
            }
  
        }
       
        
        /// <summary>
        /// 服务端异常若不处理，则通道会失效，故服务端异常要try catch/ErrorHandle统一处理（建议统一处理）
        /// </summary>出错的信道不但不能被用于服务调用，而且不能被关闭！（此时，关闭信道不能用Close()需要用Abort()!）针对会话模式
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TestErrorNotDeal()
        {      
            using (ChannelFactory<ICalculatorNoErrorHandle> channelFactory = new ChannelFactory<ICalculatorNoErrorHandle>("Tcp_CalculatorServiceNoErrorHandle"))
            {
                ICalculatorNoErrorHandle proxy = null;
                try
                {
                    proxy = channelFactory.CreateChannel();
                    (proxy as ICommunicationObject).Closed += delegate
                    {
                        Console.WriteLine("服务代理关闭成功！");
                    };
                    proxy.TestThrowError();
                }
                catch (TimeoutException e1)
                {
                    Console.WriteLine("出现异常{0}:", e1.ToString());
                    (proxy as ICommunicationObject).Abort();//不能用Close()!!!
                }
                catch (CommunicationException e2)
                {
                    Console.WriteLine("出现异常{0}:", e2.ToString());
                    //(proxy as ICommunicationObject).Close();会报错（已处于Faluted状态）
                    (proxy as ICommunicationObject).Abort();
                }
                catch (Exception e3)
                {
                    Console.WriteLine("Error:" + e3.ToString());
                    try
                    {
                        //用ErrorHandle处理后，服务端异常不会导致通道失效
                        if (proxy != null && (proxy as ICommunicationObject).State == CommunicationState.Opened)
                        {
                            Console.WriteLine("通道状态:" + (proxy as ICommunicationObject).State + " 服务端发生异常，通道未出错，再次调用Channel");
                            Console.WriteLine(proxy.Test());//模拟服务端发生错误，异常处理和不处理的区别                                     
                        }
                        else if (proxy != null)
                        {
                            Console.WriteLine("通道状态:" + (proxy as ICommunicationObject).State + "服务端发生异常，通道出错");
                            Console.WriteLine("重新创建通道");
                            string result = channelFactory.CreateChannel().Test();
                            Console.WriteLine("重新创建通道成功，请求成功!返回{0}", result);
                        }
                    }
                    catch (Exception e2)
                    {
                        Console.WriteLine("Error:" + e2.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// ErrorHandler处理异常
        /// </summary>
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
                    Console.WriteLine(e.Message.ToString());
                    try
                    {
                        Console.WriteLine("x/y={2} when x={0} and y={1}", 4, 2, proxy.DivideInt(4, 2));
                        Console.ReadKey();
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(channelFactory.State.ToString() + "  " + ((ICommunicationObject)proxy).State.ToString());
                        Console.WriteLine(ee.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 普通方法处理异常（此时服务端接口必须用Try catch,否则通道死亡）
        /// </summary>
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

        /// <summary>
        /// 单独会话模式：每个请求创建一个通道，结束或异常时会 自动关闭和中断的服务代理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TestAutoAbortChannelWhenError()
        {
            try
            {
                Console.WriteLine("x/y={2} when x={0} and y={1}", 4, 2, ClientSystem.CCalculator().Divide(4, 2)); ;
                ClientSystem.CCalNoErHandle().TestThrowError();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        /// <summary>
        /// 限流（仅限制会话通道） 若服务代理不关，单核单线程（默认）最多创建100个MaxConcurrentSessions,本机为4核8线程处理器，故最多为800个服务代理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TestLimit()
        {
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("Tcp1_CalculatorService"))
            {
                ICalculator proxy = null;
                bool stop = false;
                for (int j = 0; j < 1000 && !stop; j++)
                {
                    try
                    {

                        proxy = channelFactory.CreateChannel();
                        Console.WriteLine("x+y={2} when x={0} and y={1}", 1, 0, proxy.Divide(1, 0));
                        Console.WriteLine("第{0}个服务代理调用成功！", j + 1);

                    }
                    catch (TimeoutException e1)
                    {
                        Console.WriteLine("出现异常{0}:", e1.ToString());
                        stop = true;
                        (proxy as ICommunicationObject).Abort();//不能用Close()!!!
                    }
                    catch (CommunicationException e2)
                    {
                        Console.WriteLine("出现异常{0}:", e2.ToString());
                        (proxy as ICommunicationObject).Abort();
                    }
                    catch (Exception e3)
                    {
                        Console.WriteLine("出现异常{0}:", e3.ToString());
                    }
                }
            }
        }
    }
}
