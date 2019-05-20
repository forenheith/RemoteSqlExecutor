using System.Collections.Generic;

namespace RemoteSqlExecutor.Interfaces
{
    public interface IListItemsBuilder
    {
        IEnumerable<ListItem> Build();
    }
}