using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MoneyMonitor.LocalStorage
{
    public class SqlLiteDataRetriever : ISqlLiteDataRetriever
    {
        private readonly SqlLiteConnection _sqlLiteConnection;

        public SqlLiteDataRetriever(SqlLiteConnection sqlLiteConnection)
        {
            _sqlLiteConnection = sqlLiteConnection;
        }

        public List<T> RetrieveData<T>(Expression<Func<T,bool>> expression = null) where T: class, new()
        {
            using (var conn = _sqlLiteConnection)
            {
                var qry = conn.GetTable<T>();

                if (expression != null)
                {
                    qry.Where(expression);
                }

                var data = qry.ToList();
                return data;
            }
        }

        public void InsertOrUpdate<T>(T entity) where T : class, new()
        {
            using (var conn = _sqlLiteConnection)
            {
                
            }
        }
    }
}