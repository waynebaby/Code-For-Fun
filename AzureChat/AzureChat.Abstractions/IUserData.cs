using AzureChat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public interface IUserData
    {
        ILink<Guid, IUser<IUserData, IUserStatus, IInbox, IOutbox>> UserLink { get; }

    }
}
