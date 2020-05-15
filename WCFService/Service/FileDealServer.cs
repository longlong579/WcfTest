using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Interface;
//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// NameSpace : Service
// FileName : FileDeal
//
// Created by : XHL at 2020/5/15 16:01:15
//
// Function : 
//
//========================================================================
namespace Service
{
    public class FileDealServer : IFileDeal
    {
        public string doSome()
        {
            return "IFileDeal：开启的另一个服务返回的结果";
        }
    }
}
