using Autofac;
using MoneyMonitor.IoC.Module;

namespace MoneyMonitor.IoC
{
    public static class DependencyContainer
    {
        private static IContainer _iocContainer;
        private static readonly object Padlock = new object();

        private static IContainer IocContainer
        {
            get
            {
                RegisterContainer();
                return _iocContainer;
            }
        }

        public static void RegisterContainer()
        {
            if (_iocContainer == null)
            {
                lock (Padlock)
                {
                    if (_iocContainer == null)
                    {
                        var cb = new ContainerBuilder();
                        cb.RegisterModule(new AutoFacModuleLoader());
                        _iocContainer = cb.Build();
                    }
                }
            }
        }

        public static T GetInstance<T>()
        {
            var container = IocContainer;
            var instance = container.Resolve<T>();

            return instance;
        }
    }
}   