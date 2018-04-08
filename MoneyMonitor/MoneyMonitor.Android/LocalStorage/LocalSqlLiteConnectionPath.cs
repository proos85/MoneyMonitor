using System.IO;
using MoneyMonitor.Droid.LocalStorage;
using MoneyMonitor.LocalStorage;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(LocalSqlLiteConnectionPath))]
namespace MoneyMonitor.Droid.LocalStorage
{
    public class LocalSqlLiteConnectionPath: ILocalSqlLiteConnectionPath
    {
        const string FileName = "LocalSql.db3";

        public string LocalConnection
        {
            get
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(path, FileName);
            }
        }
    }
}