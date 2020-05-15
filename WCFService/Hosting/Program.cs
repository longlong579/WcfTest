using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Service;
using Service.Interface;
using System.ServiceModel.Description;

namespace Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
                {
                    //host.AddServiceEndpoint(typeof(ICalculator), new WSHttpBinding(), "http://127.0.0.1:3721/calculatorservice");
                    //if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                    //{
                    //    //发布服务行为
                    //    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    //    behavior.HttpGetEnabled = true;
                    //    behavior.HttpGetUrl = new Uri("http://127.0.0.1:3721/calculatorservice/metadata");
                    //    host.Description.Behaviors.Add(behavior);
                    //}
                    host.Opened += delegate
                    {
                        Console.WriteLine("CalculatorService 已经启动，按任意键中止服务！");
                    };
                    host.Open();
                    Console.Read();
                }
            });
            Task.Factory.StartNew(() =>
            {
                using (ServiceHost host = new ServiceHost(typeof(FileDealServer)))
                {
                    //host.AddServiceEndpoint(typeof(ICalculator), new WSHttpBinding(), "http://127.0.0.1:3721/calculatorservice");
                    //if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                    //{
                    //    //发布服务行为
                    //    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    //    behavior.HttpGetEnabled = true;
                    //    behavior.HttpGetUrl = new Uri("http://127.0.0.1:3721/calculatorservice/metadata");
                    //    host.Description.Behaviors.Add(behavior);
                    //}
                    host.Opened += delegate
                    {
                        Console.WriteLine("FileDealService 已经启动，按任意键中止服务！");
                    };
                    host.Open();
                    Console.Read();
                }
            });

            Console.Read();
        }
    }
}
