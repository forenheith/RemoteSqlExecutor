using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteSqlExecutor;

namespace RemoteSqlExecutorTests
{
    [TestClass]
    public class ListItemBuilderTests
    {
        [TestMethod]
        public void CheckThatBuildMethodReturnsCorrectListItemsList()
        {
            var cm = new ConfigurationManager();
            var nodes = cm.GetNodes("//add");
            var builder = new ListItemsBuilder(nodes);
            var items = builder.Build().ToList();
        }
    }
}
