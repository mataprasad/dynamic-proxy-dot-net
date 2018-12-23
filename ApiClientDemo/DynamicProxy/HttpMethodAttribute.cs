using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientDemo.DynamicProxy
{
    public class HttpMethod : Attribute
    {
        public string Path { get; set; }

        public HttpMethod(string path)
        {
            this.Path = path;
        }

        public class Get : HttpMethod
        {
            public Get(string path) : base(path)
            {
            }
        }

        public class Post : HttpMethod
        {
            public Post(string path) : base(path)
            {
            }
        }

        public class Put : HttpMethod
        {
            public Put(string path) : base(path)
            {
            }
        }

        public class Delete : HttpMethod
        {
            public Delete(string path) : base(path)
            {
            }
        }
    }
}
