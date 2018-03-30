using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MoneyMonitor.Data.Dto;

namespace AzureFunctionBackend
{
    public static class MoneyMonitorGetExpensesFunction
    {
        [FunctionName("MoneyMonitorGetExpensesFunction")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req,
            TraceWriter log)
        {
            try
            {
                log.Info("MoneyMonitorGetExpensesFunction start");

                var expenses = new List<MoneyExpenseDto>
                {
                    new MoneyExpenseDto{NameExpense = "Expense1", TypeExpense = ExpenseTypes.Charity, ValueExpense = 10},
                    new MoneyExpenseDto{NameExpense = "Expense2", TypeExpense = ExpenseTypes.Fixed, ValueExpense = 10},
                    new MoneyExpenseDto{NameExpense = "Expense2", TypeExpense = ExpenseTypes.Variable, ValueExpense = 10}
                };

                return req.CreateResponse(HttpStatusCode.OK, expenses);
            }
            catch (Exception)
            {
                // Todo: Write exception to log
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            finally
            {
                log.Info("MoneyMonitorGetExpensesFunction end");
            }
        }
    }
}
