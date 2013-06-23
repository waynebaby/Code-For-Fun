using AzureChat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public interface IChannel<out TChannelData, out TChannelStatus> :
        IObservable<IMessage<object, IMessageControlData>>
        where TChannelStatus : IChannelStatus
        where TChannelData : IChannelData
    {
        TChannelData Data
        {
            get;

        }

        TChannelStatus Status
        {
            get;

        }

        Guid Id
        {
            get;
            set;
        }

        ILink<Guid, IChannelHubService> ChannelHubServiceLink
        {
            get;
        }
        bool IsRemote { get; }

    }
}
