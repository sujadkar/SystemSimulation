using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfRestfulServiceComp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get base address from appsettings in configuration.
            Uri baseAddress = new Uri(ConfigurationManager.AppSettings["baseAddress"]);

            // Create a ServiceHost for the CalculatorService type and provide the base address.
            using (ServiceHost serviceHost = new ServiceHost(typeof(HttpService), baseAddress))
            {
                // Open the ServiceHostBase to create listeners and start listening for messages.
                serviceHost.Open();

                // The service can now be accessed.
                Console.WriteLine("The service is ready to accept WebHttp Requests.....");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                // Close the ServiceHostBase to shutdown the service.
                serviceHost.Close();
            }
        }
    }
}
