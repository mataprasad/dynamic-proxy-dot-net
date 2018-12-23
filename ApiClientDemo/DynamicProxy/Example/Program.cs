using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using System.Collections.Specialized;

namespace ApiClientDemo.DynamicProxy.Example
{
    public class Program
    {
        private static readonly ProxyGenerator _generator = new ProxyGenerator();
        static void Main(string[] args)
        {
            var a = new UriTemplate("{id}/{id2}");
            var b = new Uri("http://www.google.com");
            //b = a.BindByPosition(b, "10", "12");
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("id", "34");
            parameters.Add("id2", "43");
            b = a.BindByName(b, parameters);
            var c = b.ToString().SetQueryParam("mata", "pita");

            Console.WriteLine(c.ToString());

            return;

            //IPeopleService service = HttpProxy.Create(default(IPeopleService), "mata");
            IPeopleService service = Create<IPeopleService>();

            var resp = service.GetPeople().Execute();
            var data = resp.Value;
            var msg = resp.CoreMessage;


        }

        public static T Create<T>() where T : class
        {
            return _generator.CreateInterfaceProxyWithoutTarget<T>(new RestInterceptor());
        }
    }
}
