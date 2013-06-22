using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public interface IChannelData
    {
        ILink<Guid, IChannel<IChannelData, IChannelStatus>> ChannelLink
        {
            get;
            set;
        }

        String Name
        {
            get;
            set;
        }



    }
}
