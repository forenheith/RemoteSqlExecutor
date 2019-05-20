using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using RemoteSqlExecutor.Interfaces;

namespace RemoteSqlExecutor.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private readonly IConfigurationManager _configurationManager;
        private List<ListItem> _listItems;

        public MainViewModel(IConfigurationManager configurationManager, IListItemsBuilder listItemsBuilder)
        {
            _configurationManager = configurationManager;

            ListItems =  listItemsBuilder.Build().ToList(); 
        }

        public List<ListItem> ListItems
        {
            get { return _listItems; }
            set { this.RaiseAndSetIfChanged(ref _listItems, value, nameof(ListItems)); }
        }
    }
}