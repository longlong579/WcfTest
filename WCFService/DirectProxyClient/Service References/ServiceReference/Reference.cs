﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DirectProcyClient.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://WWW.HZZG.com/", ConfigurationName="ServiceReference.CalculatorService")]
    public interface CalculatorService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WWW.HZZG.com/CalculatorService/Add", ReplyAction="http://WWW.HZZG.com/CalculatorService/AddResponse")]
        double Add(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WWW.HZZG.com/CalculatorService/Add", ReplyAction="http://WWW.HZZG.com/CalculatorService/AddResponse")]
        System.Threading.Tasks.Task<double> AddAsync(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WWW.HZZG.com/CalculatorService/Substract", ReplyAction="http://WWW.HZZG.com/CalculatorService/SubstractResponse")]
        double Substract(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WWW.HZZG.com/CalculatorService/Substract", ReplyAction="http://WWW.HZZG.com/CalculatorService/SubstractResponse")]
        System.Threading.Tasks.Task<double> SubstractAsync(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WWW.HZZG.com/CalculatorService/Mutiply", ReplyAction="http://WWW.HZZG.com/CalculatorService/MutiplyResponse")]
        double Mutiply(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WWW.HZZG.com/CalculatorService/Mutiply", ReplyAction="http://WWW.HZZG.com/CalculatorService/MutiplyResponse")]
        System.Threading.Tasks.Task<double> MutiplyAsync(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WWW.HZZG.com/CalculatorService/Divide", ReplyAction="http://WWW.HZZG.com/CalculatorService/DivideResponse")]
        double Divide(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WWW.HZZG.com/CalculatorService/Divide", ReplyAction="http://WWW.HZZG.com/CalculatorService/DivideResponse")]
        System.Threading.Tasks.Task<double> DivideAsync(double x, double y);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CalculatorServiceChannel : DirectProcyClient.ServiceReference.CalculatorService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CalculatorServiceClient : System.ServiceModel.ClientBase<DirectProcyClient.ServiceReference.CalculatorService>, DirectProcyClient.ServiceReference.CalculatorService {
        
        public CalculatorServiceClient() {
        }
        
        public CalculatorServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CalculatorServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalculatorServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalculatorServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public double Add(double x, double y) {
            return base.Channel.Add(x, y);
        }
        
        public System.Threading.Tasks.Task<double> AddAsync(double x, double y) {
            return base.Channel.AddAsync(x, y);
        }
        
        public double Substract(double x, double y) {
            return base.Channel.Substract(x, y);
        }
        
        public System.Threading.Tasks.Task<double> SubstractAsync(double x, double y) {
            return base.Channel.SubstractAsync(x, y);
        }
        
        public double Mutiply(double x, double y) {
            return base.Channel.Mutiply(x, y);
        }
        
        public System.Threading.Tasks.Task<double> MutiplyAsync(double x, double y) {
            return base.Channel.MutiplyAsync(x, y);
        }
        
        public double Divide(double x, double y) {
            return base.Channel.Divide(x, y);
        }
        
        public System.Threading.Tasks.Task<double> DivideAsync(double x, double y) {
            return base.Channel.DivideAsync(x, y);
        }
    }
}
