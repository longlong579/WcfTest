using Artech.BatchingHosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// NameSpace : WinFormClient.RealProx
// FileName : RealProxFactory
//
// Created by : XHL at 2020/5/20 16:33:10
//
// Function : 单次调用Channel通信，并自动关闭,客户端通道管理器
//
//========================================================================
namespace WCF.XHL
{
    public class ClientSystem
    {
        const string endTcp = "Tcp_CalculatorServiceNoErrorHandle";//总节点配置信息
        const string endTcp2 = "Tcp1_CalculatorService";//总节点配置信息
        public static ICalculatorNoErrorHandle CCalNoErHandle()
        {
            //注意：每次调用必须2条语句同时使用，创建新的通道,自动关闭通道
            ServiceProxy<ICalculatorNoErrorHandle> serviceProxy = new ServiceProxy<ICalculatorNoErrorHandle>(endTcp);
            return serviceProxy.Channel;
        }
        public static ICalculator CCalculator()
        {
            //注意：每次调用必须2条语句同时使用，创建新的通道,自动关闭通道
            ServiceProxy<ICalculator> serviceProxy = new ServiceProxy<ICalculator>(endTcp2);
            return serviceProxy.Channel;
        }

    }
}
