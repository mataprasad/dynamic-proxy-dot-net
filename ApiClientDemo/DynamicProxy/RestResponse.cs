using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientDemo.DynamicProxy
{
    public class RestCall<T> where T : class
    {
        private HttpClient restClient = null;
        private HttpResponseMessageExt coreMessage = null;
        private HttpMethod httpMethod = null;

        public RestCall(string baseUrl)
        {
            this.restClient = new HttpClient();
        }

        public RestResponse<T> Execute()
        {
            return new RestResponse<T>();
        }
    }
    public class RestResponse<T> where T : class
    {
        private HttpResponseMessageExt coreMessage = null;

        public T Value
        {
            get
            {
                if (coreMessage != null)
                {
                    try
                    {
                        return coreMessage.coreMessage.Content.ReadAsAsync<T>().Result;
                    }
                    catch (Exception)
                    {
                        // log error
                    }
                }
                return default(T);
            }
        }

        public HttpResponseMessageExt CoreMessage
        {
            get
            {
                return coreMessage;
            }
        }
    }

    public class HttpResponseMessageExt
    {
        internal HttpResponseMessage coreMessage;

        public HttpResponseHeaders Headers
        {
            get
            {
                return coreMessage.Headers;
            }
        }
        public bool IsSuccessStatusCode { get; }
        public string ReasonPhrase { get; set; }
        public HttpRequestMessage RequestMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Version Version { get; set; }
    }
}
