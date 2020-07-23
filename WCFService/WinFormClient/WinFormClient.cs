using Artech.BatchingHosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormClient
{
    public partial class WinFormClient : Form
    {
        public WinFormClient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 限流（仅限制会话通道） 若服务代理不关，单核单线程（默认）最多创建100个MaxConcurrentSessions,本机为4核8线程处理器，故最多为800个服务代理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXL_Click(object sender, EventArgs e)
        {
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("Tcp1_CalculatorService"))
            {
                ICalculator proxy=null;
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
        
        /// <summary>
        /// 服务端异常若不处理，则通道会失效，故服务端异常要try catch/ErrorHandle统一处理（建议统一处理）
        /// </summary>出错的信道不但不能被用于服务调用，而且不能被关闭！（此时，关闭信道不能用Close()需要用Abort()!）针对会话模式
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestServerError_Click(object sender, EventArgs e)
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
        /// 自动关闭和中断的服务代理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSingleAutoCloseAbortProxy_Click(object sender, EventArgs e)
        {
            try
            {
                ClientSystem.CCalNoErHandle().TestThrowError();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ClientSystem.CCalculator().Divide(3,2);
                ClientSystem.CCalculator().Mutiply(3, 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
