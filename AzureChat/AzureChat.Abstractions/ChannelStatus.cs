using AzureChat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public interface IChannelStatus
    {
        ILink<Guid, IChannel<IChannelData, IChannelStatus>> ChannelLink
        {
            get;
            set;
        }
    }
}
