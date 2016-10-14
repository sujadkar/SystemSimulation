using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace WcfRestfulServiceComp
{
    public class HttpService : IHttpService
    {

        public void CreatePerson(string id)
        {
            SendMessageToQueue(".\\private$\\SystemSimulationQueue1", id);
        }

        private static void SendMessageToQueue(string queueName, string id)
        {

            // check if queue exists, if not create it

            MessageQueue msMq = null;

            if (!MessageQueue.Exists(queueName))
            {
                msMq = MessageQueue.Create(queueName);
            }
            else
            {
                msMq = new MessageQueue(queueName);
                msMq.MessageReadPropertyFilter.Priority = true;
            }
            try
            {

                Person p1 = new Person
                {
                    PersonID = Int32.Parse(id),
                    Name = "Christos",
                    Address = "Athens",
                    Age = 27
                };

                Person p2 = new Person
                {
                    PersonID = Int32.Parse(id) + 25,
                    Name = "Alex",
                    Address = "USA",
                    Age = 25
                };

                Message msg1 = new Message(p1);
                msg1.Label = p1.Name;
                msg1.Priority = MessagePriority.Normal;

                Message msg2 = new Message(p2);
                msg2.Label = p2.Name;
                msg2.Priority = MessagePriority.Normal;

                msMq.Send(msg1);
                Console.WriteLine("Person object sent with ID  " + p1.PersonID);

                msMq.Send(msg2);
                Console.WriteLine("Person object sent with ID  " + p2.PersonID);

                Console.WriteLine();


            }
            catch (MessageQueueException ee)
            {
                Console.Write(ee.ToString());
            }
            catch (Exception eee)
            {
                Console.Write(eee.ToString());
            }
            finally
            {
                msMq.Close();
            }
            Console.WriteLine("Message sent ......");

        }
    }
}
