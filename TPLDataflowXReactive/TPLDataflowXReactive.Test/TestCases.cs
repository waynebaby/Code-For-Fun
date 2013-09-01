using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TPLDataflowXReactive.Test
{
    [TestClass]
    public class TestCases
    {
        [TestMethod]
        public async Task TestBufferBlock_AsyncIn_ObservableOut()
        {

            var outputList = new List<int>();
            var bcontext = new TPLDataflowXReactive.TPLContextDefault<BufferBlock<int>, int>();
            var task = new Task(() => { });
            var d1 = bcontext.CurrentObservable.Subscribe(e =>
                {
                    outputList.Add(e);
                    task.Start();
                });

            await bcontext.CurrentBlock.SendAsync(1);
            await task;
            Assert.AreEqual(outputList[0], 1);



        }

        [TestMethod]
        public async Task TestBufferBlock_ObserverIn_AsyncOut()
        {


            var bcontext = new TPLDataflowXReactive.TPLContextDefault<BufferBlock<int>, int>();
            bcontext.CurrentObserver.OnNext(1);

            var outval = await bcontext.CurrentBlock.ReceiveAsync();


            Assert.AreEqual(outval, 1);



        }



        [TestMethod]
        public async Task TestBufferBlock_ObserverIn_AsyncObservableBothOut()
        {


            var outputList = new List<int>();
            var bcontext = new TPLDataflowXReactive.TPLContextDefault<BufferBlock<int>, int>();
            var task = new Task(() => { });


            bcontext.CurrentObserver.OnNext(1);

            var outval = await bcontext.CurrentBlock.ReceiveAsync();
            Assert.AreEqual(outval, 1);


            var d1 = bcontext.CurrentObservable.Subscribe(e =>
            {
                outputList.Add(e);
                task.Start();
            });

            bcontext.CurrentObserver.OnNext(2);

            await task;
            Assert.AreEqual(outputList[0], 2);

            task = new Task(() => { });
            var toutval2 = bcontext.CurrentBlock.ReceiveAsync();
            bcontext.CurrentObserver.OnNext(3);

            Assert.AreEqual(outputList[1], 3);
            //outval = await toutval2;  <== if observer is attached, the receive async will never work; this line will idle the process


        }
    }
}
