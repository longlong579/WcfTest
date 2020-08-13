using System;
using System.ServiceModel;
using Artech.BatchingHosting;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace Hosting
{
    class Program
    {
        static Timer timer = new Timer(state => GC.Collect(), null, 0, 100);//测试服务实例的回收，正式变成去掉
        static void Main(string[] args)
        {
            //启动多服务
            using (ServiceHostCollection hosts = new ServiceHostCollection())
            {
                foreach (ServiceHost host in hosts)
                {
                    host.Opened += (sender, arg) =>
                    {
                        Console.WriteLine("服务{0}开始监听", (sender as ServiceHost).Description.ServiceType);
                        PrintServerInfo(host);
                        Console.WriteLine();
                        Console.WriteLine();
                    };
                    
                }
                hosts.Open();
                Console.Read();
            }
        }

        static int i = 0;
        static int j = 0;
        public static void PrintServerInfo(ServiceHost host)
        {
            //不同的监听地址，对应不同的分发器，若通过ListernUri指定相同的监听地址，则会对应同一个分发器，（物理地址不一致的情况下）不同的EndPonitDispatcher
            //只有共享ListenUrl 才会一个ChannelDispatchers对应多个EndpointDispatcher
            foreach (ChannelDispatcher channelDispatcher in host.ChannelDispatchers)
            {
                Console.WriteLine("ChannelDispatcher {0} ({1})", ++i, channelDispatcher.Listener.Uri);
                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                    Console.WriteLine("\t EndpointDispatcher{0} ({1})", ++j, endpointDispatcher.EndpointAddress.Uri);
                }
            }
        }
    }
}
