using System;
using SQLite;

namespace MoneyMonitor.LocalStorage
{
    public interface ISqlLiteConnection: IDisposable
    {
        TableQuery<T> GetTable<T>() where T : class, new();
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class SqlLiteConnection : ISqlLiteConnection
    {
        private readonly ILocalSqlLiteConnectionPath _localSqlLiteConnectionPath;
        private SQLiteConnection _conn;

        public SqlLiteConnection(ILocalSqlLiteConnectionPath localSqlLiteConnectionPath)
        {
            _localSqlLiteConnectionPath = localSqlLiteConnectionPath;
        }

        public TableQuery<T> GetTable<T>() where T: class, new()
        {
            OpenConnection();
            return _conn.Table<T>();
        }

        void OpenConnection()
        {
            if (_conn == null)
            {
                _conn = new SQLiteConnection(_localSqlLiteConnectionPath.LocalConnection);
            }
        }

        public void Dispose()
        {
            _conn?.Dispose();
        }
    }
}
