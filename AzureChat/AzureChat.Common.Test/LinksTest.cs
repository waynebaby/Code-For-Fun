using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AzureChat.Common.Test
{
    [TestClass]
    public class LinksTest
    {
        [TestMethod]
        public void TestCreateAndClean()
        {
            var l = new SimpleLink<int, int>(x => Task.FromResult(x * 10));


        }
    }
}
