using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientDemo.DynamicProxy.Parameters
{
    public class PathAttribute : ValueAttribute
    {
        public PathAttribute(string value)
        {
            this.Value = value;
        }
    }
}
