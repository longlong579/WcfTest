using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// NameSpace : Artech.BatchingHosting.ErrorHandle
// FileName : WCF_ExceptionHandler
//
// Created by : XHL at 2020/7/24 9:06:03
//
// Function : 
//
//========================================================================
namespace Artech.BatchingHosting.ErrorHandle
{
    /// <summary> 
    /// WCF服务端异常处理器 
    /// 可以提供多个异常处理器做不同的操作，最简单就一个处理器，不区分异常类型（如此类所示）
    /// </summary> 
    public class WCF_ExceptionHandler : IErrorHandler
    {
        #region IErrorHandler Members

        /// <summary> 
        /// HandleError 
        /// </summary> 
        /// <param name="ex">ex</param> 
        /// <returns>true</returns> 
        /// 返回True,代表不中止通道
        public bool HandleError(Exception ex)
        {
            return true;
        }

        /// <summary> 
        /// ProvideFault 
        /// </summary> 
        /// <param name="ex">ex</param> 
        /// <param name="version">version</param> 
        /// <param name="msg">msg</param> 
        public void ProvideFault(Exception ex, MessageVersion version, ref Message msg)
        {
            // 
            //在这里处理服务端的消息，将消息写入服务端的日志 
            // 
            string err = string.Format("调用WCF接口 '{0}' 出错", ex.TargetSite.Name);
            var newEx = new FaultException(err);//直接指定错误信息（服务的调用者只能得到一段自定义的错误消息文本）

            MessageFault msgFault = newEx.CreateMessageFault();
            msg = Message.CreateMessage(version, msgFault, newEx.Action);
        }

        #endregion
    }

}
