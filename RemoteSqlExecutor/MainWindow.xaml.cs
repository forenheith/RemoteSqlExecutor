using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ReactiveUI;
using RemoteSqlExecutor.Interfaces;
using RemoteSqlExecutor.ViewModels;
using Unity;

namespace RemoteSqlExecutor
{
    public partial class MainWindow : Window, IViewFor<MainViewModel>
    {
        public MainWindow()
        {
            var container = App.Container;

            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(this.OneWayBind(ViewModel, vm => vm.ListItems, v => v.storesGrid.ItemsSource));
                var configurationManager = container.Resolve<IConfigurationManager>();

                d(this.BindCommand(ViewModel, vm=>vm.ExecuteSqlComand, v=>v.executeButton));
                var listItemsBuilder = container.Resolve<IListItemsBuilder>();

                ViewModel = new MainViewModel(configurationManager, listItemsBuilder);

            });
        }


        private void AddColumns(string[] headers)
        {
            var myGridView = new GridView
            {
                AllowsColumnReorder = true,
                ColumnHeaderToolTip = "configuration file content"
            };

            foreach (var header in headers)
            {
                AddColumn(myGridView, header, header, 100);
            }


            //databasesList.View = myGridView;
        }

        private static void AddColumn(GridView myGridView, string bindingName, string header, int width)
        {
            var gvc1 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding(bindingName),
                Header = header,
                Width = width
            };

            myGridView.Columns.Add(gvc1);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainViewModel) value; }
        }

        public MainViewModel ViewModel { get; set; }
    }
}