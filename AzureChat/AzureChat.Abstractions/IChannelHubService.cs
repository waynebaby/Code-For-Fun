using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureChat.Abstractions
{
    public interface IChannelHubService 
    {

        Guid Id { get; }
        Task<IEnumerable<IChannel<IChannelData, IChannelStatus>>> GetOnlineChannelsSnapshotAsync();

        bool TryAddAddLocalChannel<TChannel>(TChannel channel) where TChannel : IChannel<IChannelData, IChannelStatus>;
    }
}
