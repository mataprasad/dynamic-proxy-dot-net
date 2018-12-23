using ApiClientDemo.DynamicProxy.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ApiClientDemo.DynamicProxy;

namespace ApiClientDemo.DynamicProxy
{
    public class HttpProxy : RealProxy
    {
        private string baseUrl = null;

        private HttpProxy(Type t, string baseUrl) : base(t)
        {
            this.baseUrl = baseUrl;
        }

        public static T Create<T>(T target, string baseUrl)
        {
            return (T)new HttpProxy(target.GetType(), baseUrl).GetTransparentProxy();
        }

        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            if (methodCall != null)
            {
                var obj = Activator.CreateInstance(methodInfo.ReturnType, this.baseUrl);
                return new ReturnMessage(obj, methodCall.Args, methodCall.ArgCount, methodCall.LogicalCallContext, methodCall);
            }
            return new ReturnMessage(new Exception(""), methodCall);
        }
    }
}
