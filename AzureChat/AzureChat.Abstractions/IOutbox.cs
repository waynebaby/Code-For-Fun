using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureChat.Abstractions
{
    public interface IOutbox :
        IDisposable ,
        IObservable<IMessage<object ,IMessageControlData >> ,
        IObserver<IMessage<object ,IMessageControlData >> 
    {
        int QueueLength
        {
            get;

        }

        void QueueSend(IMessage<object, IMessageControlData> message);
    }
}
