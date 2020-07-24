using Artech.BatchingHosting.ErrorHandle;
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
    #region 服务端异常不处理的服务 通道死亡 通道状态为Fault
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]//若config中也有配置，则只要有一个为true,结果都是true
    public class CalculatorServiceNoErrorHandle : ICalculatorNoErrorHandle
    {
        public string Test()
        {
            return "正确返回";
        }

        public string TestThrowError()
        {
            int x = 0;
            return (5 / x).ToString();
        }
    }
    #endregion

    #region  服务端异常处理（普通处理） 通道不死亡，可以继续使用
    //IDisposable 用来测试上下文的创建于回收，正式环境无需IDisposable
    public class CalculatorService : ICalculator, IDisposable
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
            try
            {
                Console.WriteLine("{0}:操作方法被调用", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(100);
                return x / y;
            }
            catch (Exception e)
            {
                //可以不用错误契约，若要自定义错误类型(屏蔽服务端异常，转换为自定义的具体异常类型)，一定要在方法上加错误契约[FaultContract(typeof(**))]
                throw new FaultException("除0操作");
            }
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
            Console.WriteLine("{0}:构造器被调用", Thread.CurrentThread.ManagedThreadId);
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
    #endregion

    #region ErrorHandle服务 错误高级处理
   // [ServiceBehavior(IncludeExceptionDetailInFaults =true)]
    [WCF_ExceptionBehaviour(typeof(WCF_ExceptionHandler))]
    public class ErrorHandleService : IErrorHandleTest
    {
     
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

    }
    #endregion

    #region 文件服务
    public class FileDealServer : IFileDeal
    {
        public string doSome()
        {
            return "IFileDeal：开启的另一个服务返回的结果";
        }
    }
    #endregion
}
