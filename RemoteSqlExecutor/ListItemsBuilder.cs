// 201905130:59ListItemsBuilder.csРоман Евсеев201905130:59

using System.Collections.Generic;
using System.Xml;
using RemoteSqlExecutor;
using RemoteSqlExecutor.Interfaces;

namespace RemoteSqlExecutor
{
    public class ListItemsBuilder: IListItemsBuilder
    {
        private readonly XmlNodeList _xmlNodesList;

        public ListItemsBuilder(XmlNodeList xmlNodesList)
        {
            _xmlNodesList = xmlNodesList;
        }

        public IEnumerable<ListItem> Build()
        {
            foreach (XmlNode xmlNode in _xmlNodesList)
            {
                if (xmlNode.Attributes != null)
                    yield return new ListItem
                    {
                        Id = xmlNode.Attributes["ID"].Value,
                        DbName = xmlNode.Attributes["DBName"].Value,
                        DisplayName = xmlNode.Attributes["DisplayName"].Value,
                        Host = xmlNode.Attributes["Host"].Value,
                        Alias = xmlNode.Attributes["Alias"].Value,
                    };
            }
        }
    }
}