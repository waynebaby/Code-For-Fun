using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public interface IChannelData 
    {
        Guid ChannelId
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
