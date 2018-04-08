using System;
using System.IO;
using MoneyMonitor.iOS.LocalStorage;
using Xamarin.Forms;
using MoneyMonitor.LocalStorage;

[assembly: Dependency(typeof(LocalSqlLiteConnectionPath))]
namespace MoneyMonitor.iOS.LocalStorage
{
    public class LocalSqlLiteConnectionPath: ILocalSqlLiteConnectionPath
    {
        const string FileName = "LocalSql.db3";

        public string LocalConnection
        {
            get
            {
                string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string libFolder = Path.Combine(docFolder, "..", "Library");

                if (!Directory.Exists(libFolder))
                {
                    Directory.CreateDirectory(libFolder);
                }

                return Path.Combine(libFolder, FileName);
            }
        }
    }
}