using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{


    public interface IMessage<out TMessageBody, out TMessageControlData>
        where TMessageControlData : IMessageControlData
    {
        TMessageControlData ControlData { get; }
        TMessageBody Body { get; }
       
    }
}
