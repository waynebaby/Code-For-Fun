using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public interface IMessageControlData
    {

        bool IsPrivate { get; }
        Guid? FromUser { get; }
        Guid? ToUser { get; }
        DateTime SendTime { get; }
    }
}
