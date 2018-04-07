using System;

namespace MoneyMonitor.Client.Base
{
    public abstract class BaseClient
    {
        protected Uri BaseUrl => new Uri("https://moneymonitorapp.azurewebsites.net/");
    }
}