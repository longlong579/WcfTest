﻿using System;
using System.ServiceModel;
using System.Collections.ObjectModel;
using Artech.BatchingHosting.Configuration;

namespace Artech.BatchingHosting
{
    /// <summary>
    /// 多服务启动同时处理
    /// </summary>
    public class ServiceHostCollection : Collection<ServiceHost>, IDisposable
    {
        public ServiceHostCollection(params Type[] serviceTypes)
        {
            BatchingHostingSettings settings = BatchingHostingSettings.GetSection();
            foreach (ServiceTypeElement element in settings.ServiceTypes)
            {
                this.Add(element.ServiceType);
            }

            if (null != serviceTypes)
            { 
                Array.ForEach<Type>(serviceTypes, serviceType=> this.Add(new ServiceHost(serviceType)));
            }
        }
        public void Add(params Type[] serviceTypes)
        {
            if (null != serviceTypes)
            {
                Array.ForEach<Type>(serviceTypes, serviceType => this.Add(new ServiceHost(serviceType)));
            }
        }
        public void Open()
        {
            foreach (ServiceHost host in this)
            {
                host.Open();
            }
        }
        public void Dispose()
        {
            foreach (IDisposable host in this)
            {
                host.Dispose();
            }
        }
    }
}
