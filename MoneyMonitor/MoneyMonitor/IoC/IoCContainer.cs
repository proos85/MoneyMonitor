﻿using Autofac;
using MoneyMonitor.Client.Overview;
using MoneyMonitor.Pages;

namespace MoneyMonitor.IoC
{
    public static class IoCContainer
    {
        private static IContainer _iocContainer;
        private static readonly object Padlock = new object();

        public static IContainer IocContainer
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

    public class AutoFacModuleLoader: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MockOverviewClient>().As<IOverviewClient>();
            builder.RegisterType<MoneyOverviewPage>().AsSelf();
        }
    }
}   