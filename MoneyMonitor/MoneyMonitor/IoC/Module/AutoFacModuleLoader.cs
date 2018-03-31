using Autofac;
using MoneyMonitor.Authentication;
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

            RegisterB2CAuthentication(builder);
            RegisterClients(builder);
            RegisterViewModels(builder);
            RegisterPages(builder);
        }

        private static void RegisterB2CAuthentication(ContainerBuilder builder)
        {
            builder.RegisterType<B2CAuthenticationProvider>().As<IB2CAuthenticationProvider>();
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
            builder.RegisterType<LoginPage>().AsSelf();
            builder.RegisterType<MoneyOverviewPage>().AsSelf();
        }
    }
}