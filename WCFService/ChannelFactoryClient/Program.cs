using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Interface;
using System.ServiceModel;

namespace ChannelFactoryClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (ChannelFactory<ICalculator> channelFactory =
            //    new ChannelFactory<ICalculator>(new WSHttpBinding(), "http://127.0.0.1:3721/calculatorservice"))
            //{
            //    ICalculator proxy = channelFactory.CreateChannel();
            //    Console.WriteLine("x+y={2} when x={0} and y={1}", 1, 2, proxy.Add(1, 2));
            //    Console.WriteLine("x/y={2} when x={0} and y={1}", 1, 0, proxy.Divide(1, 0));
            //    Console.Read();
            //}
            Task.Factory.StartNew(() =>
            {
                using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("WSHttp_CalculatorService"))
                {
                    for (int i = 0; i < 1; i++)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            ICalculator proxy = channelFactory.CreateChannel();
                            Console.WriteLine("x/y={2} when x={0} and y={1}", 1, 0, proxy.Divide(1, 0));
                        });
                    }
                    Console.Read();
                }
            });

            Task.Factory.StartNew(() =>
            {
                using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("Http_CalculatorService"))
                {
                    for (int i = 0; i < 1; i++)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            ICalculator proxy = channelFactory.CreateChannel();
                            Console.WriteLine(proxy.doSome());
                        });
                    }
                    Console.Read();
                }
            });
            Task.Factory.StartNew(() =>
            {
                using (ChannelFactory<IFileDeal> channelFactory = new ChannelFactory<IFileDeal>("WSHttp_FileService"))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            IFileDeal proxy = channelFactory.CreateChannel();
                            Console.WriteLine(proxy.doSome());
                        });
                    }
                    Console.Read();
                }
            });
            Console.Read();
        }
    }
}
