using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientDemo.DynamicProxy
{
    public class RestInterceptor : IInterceptor
    {
        public RestInterceptor()
        {
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.ReturnValue = null;
            var methodInfo = invocation.Method;
            if (methodInfo != null)
            {
                var obj = Activator.CreateInstance(methodInfo.ReturnType, "mata");
                invocation.ReturnValue = obj;
                //return new ReturnMessage(obj, methodCall.Args, methodCall.ArgCount, methodCall.LogicalCallContext, methodCall);
            }


            /*
            // Build Request
            var methodInfo = new RestMethodInfo(invocation.Method); // TODO: Memoize these objects in a hash for performance
            var request = new RequestBuilder(methodInfo, invocation.Arguments).Build();

            // Execute request
            var responseType = invocation.Method.ReturnType;
            var genericTypeArgument = responseType.GenericTypeArguments[0];
            // We have to find the method manually due to limitations of GetMethod()
            var methods = restClient.GetType().GetMethods();
            MethodInfo method = methods.Where(m => m.Name == "Execute").First(m => m.IsGenericMethod);
            MethodInfo generic = method.MakeGenericMethod(genericTypeArgument);
            invocation.ReturnValue = generic.Invoke(restClient, new object[] { request });
            */
        }
    }
}
