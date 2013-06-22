using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AzureChat.Abstractions
{
    public interface IInbox :
        IObserver<AzureChat.Abstractions.IMessage<object, IMessageControlData>>,
        IObservable<OneOrMore<AzureChat.Abstractions.IMessage<object, IMessageControlData>>>,
        IDisposable
    {
        //Task<System.Collections.Generic.IEnumerable<AzureChat.Abstractions.IMessage<object, IMessageControlData>>> TryReceive();


    }
}
