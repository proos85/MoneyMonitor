using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using MoneyMonitor.Data.Dto;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace MoneyExpenseFunctionApp
{
    // ReSharper disable once UnusedMember.Global
    public static class GetExpenses
    {
        [FunctionName("GetExpenses")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")]HttpRequestMessage req, 
            [Table("MoneyExpenses", Connection = "AzureWebJobsStorage")]IQueryable<MoneyExpenses> inTable, 
            TraceWriter log)
        {
            try
            {
                log.Info("Start MoneyExpenses");

                var expenses = inTable
                    .ToList()
                    .Select(x => new MoneyExpenseDto
                    {
                        NameExpense = x.NameExpense,
                        ValueExpense = x.ValueExpense,
                        TypeExpense = (ExpenseTypes) x.TypeExpenseValue
                    });

                return req.CreateResponse(HttpStatusCode.OK, expenses);
            }
            catch (Exception exception)
            {
                log.Info($"The following exception occurred: {exception}");
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, exception);
            }
            finally
            {
                log.Info("End MoneyExpenses");
            }
        }

        // ReSharper disable once ClassNeverInstantiated.Global
        public class MoneyExpenses : TableEntity
        {
            public string NameExpense { get; set; }
            public int TypeExpenseValue { get; set; }
            public double ValueExpense { get; set; }
        }
    }
}
