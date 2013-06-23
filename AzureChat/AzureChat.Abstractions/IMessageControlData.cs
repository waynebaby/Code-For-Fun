using AzureChat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public interface IMessageControlData
    {

        bool IsPrivate { get; }
        ILink<Guid, IUser<IUserData, IUserStatus, IInbox, IOutbox>> FromUserLink { get; }
        ILink<Guid, IUser<IUserData, IUserStatus, IInbox, IOutbox>> ToUserLink { get; }
        DateTime SendTime { get; }
    }
}
