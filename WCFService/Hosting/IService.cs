using Artech.BatchingHosting.ErrorHandle;
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
// NameSpace : Artech.BatchingHosting
// FileName : IService
//
// Created by : XHL at 2020/7/23 19:51:31
//
// Function : 
//
//========================================================================
namespace Artech.BatchingHosting
{
    #region 契约接口
    /// <summary>
    /// 未处理服务端的异常=》通道死亡
    /// </summary>
    [ServiceContract(Name = "CalculatorServiceWithoutErrorHandle", Namespace = "http://WWW.HZZG.com/")]
    public interface ICalculatorNoErrorHandle
    {
        [OperationContract]
        string TestThrowError();

        [OperationContract]
        string Test();
    }



    /// <summary>
    /// 一般的异常处理方式
    /// 默认是会话模式 单例模式适合多并发，但资源消耗多（一次调用即创建一个实例），单调模式（全程只有一个实例，默认同步处理）会话模式（适合客户端少,长时间调用的情况）
    /// </summary>
    [ServiceContract(Name = "CalculatorService", Namespace = "http://WWW.HZZG.com/")]
    public interface ICalculator
    {
        [OperationContract]
        double Add(double x, double y);

        [OperationContract]
        double Substract(double x, double y);


        [OperationContract]
        double Mutiply(double x, double y);

        [OperationContract]
        double Divide(double x, double y);

        [OperationContract]
        int DivideInt(int x, int y);

        [OperationContract]
        string doSome();
    }

    /// <summary>
    /// ErrorHandle异常处理方式
    /// 默认是会话模式 单例模式适合多并发，但资源消耗多（一次调用即创建一个实例），单调模式（全程只有一个实例，默认同步处理）会话模式（适合客户端少,长时间调用的情况）
    /// </summary>
    [ServiceContract(Name = "ErrorHandleService", Namespace = "http://WWW.HZZG.com/")]
    public interface IErrorHandleTest
    {
        [OperationContract]
        int DivideInt(int x, int y);
    }


    /// <summary>
    /// 文件传输
    /// </summary>
    [ServiceContract(Name = "FileService", Namespace = "http://WWW.HZZG.com/")]
    public interface IFileDeal
    {
        [OperationContract]
        string doSome();
    }
    #endregion
}
