using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Service.Interface
{
    [ServiceContract(Name ="CalculatorService",Namespace ="http://WWW.HZZG.com/")]
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
        string doSome();
    }
}
