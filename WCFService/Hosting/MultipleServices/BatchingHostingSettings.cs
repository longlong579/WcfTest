using System;
using System.Configuration;
using System.ComponentModel;

namespace Artech.BatchingHosting.Configuration
{
    /// <summary>
    /// 多服务启动 获取Congifg 节点artech.batchingHosting中的配置信息（服务）
    /// </summary>
    public class BatchingHostingSettings: ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ServiceTypeElementCollection ServiceTypes
        {
            get { return (ServiceTypeElementCollection)this[""]; }
        }

        public static BatchingHostingSettings GetSection()
        {
            //“artech.batchingHosting”为Config中的节点，配置多个服务
            return ConfigurationManager.GetSection("artech.batchingHosting") as BatchingHostingSettings;
        }
    }
    public class ServiceTypeElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceTypeElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            ServiceTypeElement serviceTypeElement = (ServiceTypeElement)element;
            return serviceTypeElement.ServiceType.MetadataToken;
        }
    }
    public class ServiceTypeElement : ConfigurationElement
    {
        [ConfigurationProperty("type",IsRequired = true)]
        [TypeConverter(typeof(AssemblyQualifiedTypeNameConverter))]
        public Type ServiceType
        {
            get { return (Type)this["type"]; }
            set { this["type"] = value; }
        }
    }
}
