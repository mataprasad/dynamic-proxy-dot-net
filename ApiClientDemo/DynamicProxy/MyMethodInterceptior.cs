using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace ApiClientDemo.DynamicProxy
{
    public class Class0 : MarshalByRefObject
    {
        public static T CreateProxy<T>()
        {
            return (T)new RealProxyImpl<T>(Activator.CreateInstance<T>()).GetTransparentProxy();
        }
    }
    [MethodInterceptor]
    public class Class1 : Class0
    {
        [MethodInterceptor]
        public void AA(string m, string b)
        {
            Debug.WriteLine(m + " -AA- " + b);
        }

        public void BB(string m, string b)
        {
            Debug.WriteLine(m + " -BB- " + b);
        }
    }

    public class RealProxyImpl<T> : RealProxy
    {
        private T baseObject;
        private IMethodInterceptor methodInterceptor;
        private IMethodInterceptor methodInterceptorOnClass;

        public RealProxyImpl(T obj) : base(typeof(T))
        {
            baseObject = obj;
            methodInterceptorOnClass = obj.GetType().GetCustomAttribute<IMethodInterceptor>();
        }

        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            methodInterceptor = methodInfo.GetCustomAttribute<IMethodInterceptor>();
            PreCall(msg);
            var returnValue = methodInfo.Invoke(baseObject, methodCall.Args);
            PostCall(msg, returnValue);
            return new ReturnMessage(baseObject, methodCall.Args, methodCall.ArgCount, methodCall.LogicalCallContext, methodCall);
        }

        private void PreCall(IMessage msg)
        {
            if (methodInterceptorOnClass != null)
            {
                methodInterceptorOnClass.PreToCall(msg);
            }
            if (methodInterceptor != null)
            {
                methodInterceptor.PreToCall(msg);
            }
        }
        private void PostCall(IMessage msg, object returnValue)
        {
            if (methodInterceptor != null)
            {
                methodInterceptor.PostToCall(msg, returnValue);
            }
            if (methodInterceptorOnClass != null)
            {
                methodInterceptorOnClass.PostToCall(msg, returnValue);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public abstract class IMethodInterceptor : Attribute
    {
        public abstract void PreToCall(IMessage msg);
        public abstract void PostToCall(IMessage msg, object returnValue);
    }

    public class MethodInterceptor : IMethodInterceptor
    {
        public MethodInterceptor() { }

        public override void PostToCall(IMessage msg, object returnValue)
        {
            Debug.Print("From PostToCall");
        }

        public override void PreToCall(IMessage msg)
        {
            Debug.Print("From PreToCall");
        }
    }
}
