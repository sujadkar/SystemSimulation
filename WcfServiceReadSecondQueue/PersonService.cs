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
    class PersonService : IPersonService
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void ProcessPerson(MsmqMessage<Person> ordermsg)
        {
            Person p = (Person)ordermsg.Body;
            Console.WriteLine("Processing Person ID {0} ", p.PersonID);
            Console.WriteLine("Name: " + p.Name);
            Console.WriteLine("Address: " + p.Address);
            Console.WriteLine("Age: " + p.Age);
            Console.WriteLine("Person processed......");
            Console.WriteLine("----------------------------");
            Console.WriteLine();
        }
    }
}
