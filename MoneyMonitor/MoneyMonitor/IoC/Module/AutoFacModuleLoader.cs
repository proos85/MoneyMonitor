using Autofac;
using MoneyMonitor.Client.Overview;
using MoneyMonitor.Client.Overview.Mock;
using MoneyMonitor.Pages;
using MoneyMonitor.ViewModel;

namespace MoneyMonitor.IoC.Module
{
    public class AutoFacModuleLoader: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterClients(builder);
            RegisterViewModels(builder);
            RegisterPages(builder);
        }

        private static void RegisterClients(ContainerBuilder builder)
        {
            builder.RegisterType<MockOverviewClient>().As<IOverviewClient>();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<OverviewViewModel>().AsSelf();
        }

        private static void RegisterPages(ContainerBuilder builder)
        {
            builder.RegisterType<MoneyOverviewPage>().AsSelf();
            
        }
    }
}