using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MoneyMonitor.LocalStorage
{
    public interface ISqlLiteDataRetriever
    {
        List<T> RetrieveData<T>(Expression<Func<T,bool>> expression = null) where T: class, new();
    }
}