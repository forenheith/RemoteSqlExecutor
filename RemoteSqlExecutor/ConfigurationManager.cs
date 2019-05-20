#define TEST
using System;
using System.Windows;
using System.Xml;
using RemoteSqlExecutor.Interfaces;

namespace RemoteSqlExecutor
{
    public class ConfigurationManager : IConfigurationManager
    {
        private string _fileName = "application.config";
        private static XmlDocument _config;

        public ConfigurationManager()
        {
            _config = Open();
        }

        /// <summary>
        /// Creates XmlDocument instance
        /// </summary>
        /// <returns>XmlDocument instance</returns>
        private XmlDocument Open()
        {

            var doc = new XmlDocument {PreserveWhitespace = true};
            try
            {
#if TEST
                doc.Load(
                    @"c:\Users\forenheith\Documents\Visual Studio 2015\Projects\RemoteSqlExecutor\RemoteSqlExecutor\bin\Debug\application.config");
#else
                doc.Load(_fileName);
#endif


            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("No config file found", "Error");
            }

            return doc;
        }

        /// <summary>
        /// Get nodes list by xPath
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns>XmlNodesList</returns>
        public XmlNodeList GetNodes(string xPath)
        {
            var xRoot = _config.DocumentElement;
            return xRoot?.SelectNodes(xPath);
        }

        /// <summary>
        ///  Getting attribute value of the filtered node by its name   
        /// </summary>
        /// <param name="key"></param>
        /// <param name="attributeName"></param>
        /// <param name="xPathExpression"></param>
        /// <example><code>var s = GetValue("//add", "DBName", "[@Host='10.0.0.0']");</code></example> 
        /// <returns>value of the attribute specified</returns>
        public static string GetValue(string key, string attributeName, string xPathExpression)
        {
            var xRoot = _config.DocumentElement;
            var xPath = $"{key}{xPathExpression}";
            var xmlNode = xRoot.SelectSingleNode(xPath);

            if (xmlNode?.Attributes == null)
            {
                return string.Empty;
            }

            foreach (XmlAttribute xmlNodeAttribute in xmlNode.Attributes)
            {
                if (xmlNodeAttribute.Name == attributeName)
                {
                    return xmlNodeAttribute.Value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Getting xml nodes count by xPath
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns>count of nodes specified</returns>
        private int GetNodesCount(string xPath)
        {
            var xmlNodes = GetNodes(xPath);
            return xmlNodes?.Count ?? 0;
        }
    }
}