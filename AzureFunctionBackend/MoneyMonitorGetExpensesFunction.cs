#r "Microsoft.WindowsAzure.Storage"

using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureFunctionBackend
{
    public static class MoneyMonitorGetExpensesFunction
    {
        [FunctionName("MoneyMonitorGetExpensesFunction")]
        public static HttpResponseMessage Run(HttpRequestMessage req, IQueryable<Person> inTable, TraceWriter log)
        {
            var query = from person in inTable select person;
            foreach (Person person in query)
            {
                log.Info($"Name:{person.Name}");
            }
            return req.CreateResponse(HttpStatusCode.OK, inTable.ToList());
        }
    }

    public class Person : TableEntity
    {
        public string Name { get; set; }
        public string Testje { get; set; }
    }
}
