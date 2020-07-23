using System;
using System.ServiceModel;
using System.Threading;

//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// NameSpace : Hosting
// FileName : Service
//
// Created by : XHL at 2020/5/20 10:18:28
//
// Function : 服务端若不做异常处理，错误会导致通道失效！（try catch/异常统一处理，2者选其一，建议统一处理ErrorHandle）
//
//========================================================================
namespace Artech.BatchingHosting
{
    #region 契约接口
    [ServiceContract(Name = "CalculatorServiceWithoutErrorHandle", Namespace = "http://WWW.HZZG.com/")]
    public interface ICalculatorNoErrorHandle
    {
        [OperationContract]
        string TestThrowError();

        [OperationContract]
        string Test();
    }

    /// <summary>
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


    [ServiceContract(Name = "FileService", Namespace = "http://WWW.HZZG.com/")]
    public interface IFileDeal
    {
        [OperationContract]
        string doSome();
    }
    #endregion

    #region 服务
    public class CalculatorServiceNoErrorHandle : ICalculatorNoErrorHandle
    {
        public string Test()
        {
            return "正确返回";
        }

        public string TestThrowError()
        {
            throw new NotImplementedException();
        }
    }

    //IDisposable 用来测试上下文的创建于回收，正式环境无需IDisposable
    public class CalculatorService : ICalculator,IDisposable
    {
        public double Add(double x, double y)
        {
            try
            {
                Console.WriteLine("{0}:操作方法被调用", Thread.CurrentThread.ManagedThreadId);
                throw (new Exception("我是测试异常"));
            }
            catch (Exception e)
            {
                return 0;
            }
            //return x + y;
        }
        public double Substract(double x, double y)
        {
            Console.WriteLine("{0}:操作方法被调用", Thread.CurrentThread.ManagedThreadId);
            return x - y;
        }

        /// <summary>
        /// 若服务端对异常不做处理，通道会死亡。若做了异常捕获，则通道死亡
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int DivideInt(int x, int y)
        {
                Console.WriteLine("{0}:操作方法被调用", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(100);
                return x / y;

        }
        public double Divide(double x, double y)
        {
            Console.WriteLine("{0}:操作方法被调用", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(100);
            return x / y;
        }

        public double Mutiply(double x, double y)
        {
            Console.WriteLine("{0}:操作方法被调用", Thread.CurrentThread.ManagedThreadId);
            return x * y;
        }

        public string doSome()
        {
            return "W是另一个通道返回的信息";
        }
        public CalculatorService()
        {
            Console.WriteLine("{0}:构造器被调用",Thread.CurrentThread.ManagedThreadId);
        }
        ~CalculatorService()
        {
            Console.WriteLine("{0}:终止器被调用", Thread.CurrentThread.ManagedThreadId);
        }
        public void Dispose()
        {
            Console.WriteLine("{0}:Dispose被调用", Thread.CurrentThread.ManagedThreadId);
        }
    }

    public class FileDealServer : IFileDeal
    {
        public string doSome()
        {
            return "IFileDeal：开启的另一个服务返回的结果";
        }
    }
    #endregion
}
