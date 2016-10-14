using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using System.Text;
using System.Threading.Tasks;
using WcfRestfulServiceComp;

namespace WcfServiceReadSecondQueue
{
    [ServiceContract(Namespace = "http://WcfServiceReadSecondQueue")]
    [ServiceKnownType(typeof(Person))]
    public interface IPersonService
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void ProcessPerson(MsmqMessage<Person> msg);
    }
}
