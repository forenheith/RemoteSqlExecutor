using System.Xml;

namespace RemoteSqlExecutor.Interfaces
{
    public interface IConfigurationManager
    {
        XmlNodeList GetNodes(string xPath);
    }
}