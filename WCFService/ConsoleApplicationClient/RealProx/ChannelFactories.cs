using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// NameSpace : WinFormClient
// FileName : ChannelFactories
//
// Created by : XHL at 2020/5/20 15:29:33
//
// Function : 
//
//========================================================================
namespace WCF.XHL
{
    public static class ChannelFactories
    {
        private static Dictionary<string, ChannelFactory> channelFactories = new Dictionary<string, ChannelFactory>();
        public static ChannelFactory<TChannel> GetFactory<TChannel>(string endpointConfigName)
        {
            if (channelFactories.ContainsKey(endpointConfigName))
            {
                return channelFactories[endpointConfigName] as ChannelFactory<TChannel>;
            }
            lock(channelFactories)
            {
                if (channelFactories.ContainsKey(endpointConfigName))
                {
                    return channelFactories[endpointConfigName] as ChannelFactory<TChannel>;
                }
                ChannelFactory<TChannel> channelFactory = new ChannelFactory<TChannel>(endpointConfigName);
                channelFactory.Open();
                channelFactories[endpointConfigName] = channelFactory;
                return channelFactory;
            }
        }

        //public static void CloseFactory(string endpointConfigName)
        //{
        //    lock (channelFactories)
        //    {
        //        if (channelFactories.ContainsKey(endpointConfigName))
        //        {
        //           channelFactories.Remove(endpointConfigName);
        //        }
        //    }
        //}
    }
}
