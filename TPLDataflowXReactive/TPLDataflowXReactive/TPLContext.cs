using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TPLDataflowXReactive
{
    public abstract class TPLContextBase<T>
    {
        IPropagatorBlock<T, T>
            _CurrentBlock;

        public System.Threading.Tasks.Dataflow.IPropagatorBlock<T, T> CurrentBlock
        {
            get { return _CurrentBlock; }
            set
            {
                _CurrentBlock = value;
                CurrentObservable = CurrentBlock.AsObservable();
                CurrentObserver = CurrentBlock.AsObserver();

            }
        }

        public IObserver<T> CurrentObserver { get; private set; }
        public IObservable<T> CurrentObservable { get; private set; }


    }


    public class TPLContextDefault<TBlock, TValue> : TPLContextBase<TValue> where TBlock : IPropagatorBlock<TValue, TValue>, new()
    {
        public TPLContextDefault()
        {
            CurrentBlock = new TBlock();
        }

    }
}
