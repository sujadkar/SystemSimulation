using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WcfRestfulServiceComp;

namespace ThreadsReadWcfQueue
{
    class Program
    {
        private static Object locker = new Object();

        static void Main(string[] args)
        {
            Console.WriteLine("Press w to create a new thread");
            string answer = Console.ReadLine();

            while (answer == "w")
            {
                Thread worker1 = new Thread(ReadQueue);
                worker1.Start("Christos");

                Thread worker2 = new Thread(ReadQueue);
                worker2.Start("Alex");

                worker1.Join();
                worker2.Join();
                answer = Console.ReadLine();
            }
            Console.WriteLine("Messages downloaded...");
        }

        static void ReadQueue(object label)
        {
            MessageQueue msgQ = new MessageQueue(".\\private$\\SystemSimulationQueue1");
            for (int i = 0; i < 20; i++)
            {
                ReadQueueMessagesByLabel(msgQ, label.ToString());
                Thread.Sleep(7000);
            }
        }

        private static void ReadQueueMessagesByLabel(MessageQueue msgQ, string label)
        {
            Person person = new Person();
            Object o = new Object();
            System.Type[] arrTypes = new System.Type[2];
            arrTypes[0] = person.GetType();
            arrTypes[1] = o.GetType();
            msgQ.Formatter = new XmlMessageFormatter(arrTypes);

            Message[] messages = msgQ.GetAllMessages();
            foreach (Message msg in messages)
            {
                if (msg.Label == label)
                {
                    person = (Person)msgQ.ReceiveById(msg.Id).Body;
                    Console.WriteLine();
                    Console.WriteLine("Message retreived....");
                    Console.WriteLine("Person ID: " + person.PersonID);
                    Console.WriteLine("Name: " + person.Name);
                    Console.WriteLine("Address: " + person.Address);
                    Console.WriteLine("Age: " + person.Age);

                    SendMessageToQueue(".\\private$\\SystemSimulationQueue2", person);
                    Console.WriteLine("Thread is sleeping....");
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine();
                    Thread.Sleep(3000);
                }
            }
        }

        private static void SendMessageToQueue(string queueName, Person person)
        {

            // check if queue exists, if not create it

            MessageQueue msMq = null;

            lock (locker)
            {
                if (!MessageQueue.Exists(queueName))
                {
                    msMq = MessageQueue.Create(queueName);
                }
                else
                {
                    msMq = new MessageQueue(queueName);
                    msMq.MessageReadPropertyFilter.Priority = true;
                }
            }
            try
            {


                Message msg = new Message(person);
                msg.Label = person.Name;
                msg.Priority = MessagePriority.Normal;

                msMq.Send(msg);
                Console.WriteLine("Person object pushed to second queue. Person ID: " + person.PersonID);
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

        }
    }
}
