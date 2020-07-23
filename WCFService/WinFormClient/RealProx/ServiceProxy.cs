using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// NameSpace : WinFormClient
// FileName : ServiceProxy
//
// Created by : XHL at 2020/5/20 15:36:47
//
// Function : 利用RealProxy 单次调用Channel通信，并自动关闭
//如果客户端用委托模式（WCF全面解析上册第8章节）,分层结构更清晰，但要多编写对应的接口代理，增加代码量，综合考虑，客户端短链接用代理模式
//
//========================================================================
namespace WinFormClient
{
    public class ServiceProxy<TChannel> : RealProxy
    {
        public TChannel Channel { get; private set; }
        private ICommunicationObject innerChennel;
        public ServiceProxy(string endpointConfigName)
            :base(typeof(TChannel))
        {
            ChannelFactory<TChannel> channelFactory =
                ChannelFactories.GetFactory<TChannel>(endpointConfigName);
            this.innerChennel = (ICommunicationObject)channelFactory.CreateChannel();
            Console.WriteLine("创建通道");
            this.Channel = (TChannel)this.GetTransparentProxy();
        }

        public override IMessage Invoke(IMessage msg)
        {
            IMethodCallMessage methodCall = (IMethodCallMessage)msg;
            object[] args = (object[])Array.CreateInstance(typeof(object),methodCall.ArgCount);
            methodCall.Args.CopyTo(args,0);
            try
            {
                object ret = methodCall.MethodBase.Invoke(this.innerChennel,args);
                this.innerChennel.Close();
                return new ReturnMessage(ret, args, methodCall.ArgCount, methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception ex)
            {
                Exception innerEx = ex.InnerException;
                if(null==innerEx)
                {
                    return new ReturnMessage(ex,methodCall);
                }
                if (innerEx is TimeoutException || innerEx is CommunicationException)
                {
                    Console.WriteLine("关闭通道");
                    this.innerChennel.Abort();
                }
                return new ReturnMessage(innerEx,methodCall);
            }
        }
    }
}
