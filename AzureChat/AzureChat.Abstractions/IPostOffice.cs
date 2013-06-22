using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public interface IPostOffice 
    {


        IUserHub UserHub
        {
            get;
            set;
        }

        IChannelHub ChannelHub
        {
            get;
            set;
        }
    }
}
