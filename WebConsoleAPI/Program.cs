using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace WebConsoleAPI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:3032");

            config.Routes.MapHttpRoute( "API Default", "api/{controller}/{id}", new {id = RouteParameter.Optional});
            config.Routes.MapHttpRoute( "API new", "api/{controller}/{method}/{id}", new { id = RouteParameter.Optional});

            var appXmlType =
                config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}