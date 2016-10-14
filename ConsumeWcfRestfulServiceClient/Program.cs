using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeWcfRestfulServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("System Simulation.....");
            Console.WriteLine();
            Console.WriteLine("Press c to create a webhttprequest");

            string answer = Console.ReadLine();

            while (answer == "c")
            {
                Random gen = new Random();
                int id = gen.Next(0, 1000);
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://localhost:8000/WcfRestfulServiceComp/Service/person/" + id);
                HttpWebResponse webResp = (HttpWebResponse)myReq.GetResponse();
                answer = Console.ReadLine();
            }
        }
    }
}
