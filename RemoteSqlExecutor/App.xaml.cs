using System.Windows;
using RemoteSqlExecutor.Interfaces;
using Unity;
using Unity.Injection;

namespace RemoteSqlExecutor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        public App()
        {
            Container = new UnityContainer();
            Container.RegisterType<IUnityContainer, UnityContainer>();
            Container.RegisterType<IConfigurationManager, ConfigurationManager>();
            Container.RegisterType<IListItemsBuilder, ListItemsBuilder>(new InjectionConstructor(Container.Resolve<IConfigurationManager>().GetNodes("//add")));
            Properties.Add("Container", Container);
        }

        public static UnityContainer Container;
    }
}
