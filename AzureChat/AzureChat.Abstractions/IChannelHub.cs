using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public interface IChannelHub 
    {

        Guid Id { get;  }
        IEnumerable<IChannel<IChannelData, IChannelStatus>> OnlineChannelsSnapshot { get; }
    }
}
