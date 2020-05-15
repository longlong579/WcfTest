using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Service.Interface
{
    [ServiceContract(Name ="FileService",Namespace ="http://WWW.HZZG.com/")]
    public interface IFileDeal
    {  
        [OperationContract]
        string doSome();
    }
}
