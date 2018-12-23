using ApiClientDemo.DynamicProxy.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientDemo.DynamicProxy.Example
{
    public interface IPeopleService
    {
        [HttpMethod.Get("people")]
        RestCall<List<Person>> GetPeople();

        [HttpMethod.Get("people/{id}")]
        RestCall<Person> GetPerson([Path("id")] int id);

        [HttpMethod.Get("people/{id}")]
        RestCall<Person> GetPerson([Path("id")] int id, [Query("limit")] int limit, [Query("test")] string test);

        [HttpMethod.Post("people")]
        RestCall<Person> AddPerson([Body] Person person);
    }
}
