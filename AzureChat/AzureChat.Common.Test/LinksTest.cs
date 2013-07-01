using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace AzureChat.Common.Test
{
    [TestClass]
    public class LinksTest
    {
        [TestMethod]
        public async Task TestCreateAndCleanSimple()
        {


            var l = new Link<int, int>(x => Task.FromResult(x * 10));

            Assert.IsFalse(l.IsInstanceLoaded);


            var value = await l.GetItemAsync();
            Assert.IsTrue(l.IsInstanceLoaded);
            Assert.AreEqual(value, 0); //Id =0  result=0;

            l.LinkId = 100;
            Assert.IsFalse(l.IsInstanceLoaded);

            Assert.AreEqual(l.GetItem(), 1000);

        }

        [TestMethod]
        public async Task TestCreateAndCleanConcurrent()
        {
            //var l = new SimpleLink<int, int>(x => Task.FromResult(x * 10)); ;
            var l = new SafeLink<int, int>(
                async x =>
                {
                    Debug.Print("ID:{0}", x);
                    return await Task.FromResult(x * 10);
                }
                );
            Func<Task> asyncTaskFunc = async () =>
            {
                var rnd = new Random();
                var id = rnd.Next(0, 100000);
                // l.LinkId = id;
                var result = await l.RefreshIdAndGetItemAsync(id);
                Assert.AreEqual(id * 10, result);
            };


            var tsks = Enumerable.Range(0, 10000).Select(
                     x => Task.Run(asyncTaskFunc)

                 )
                 .ToArray();
            await Task.WhenAll(tsks);


        }


    }
}
