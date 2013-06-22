using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public interface IUserStatus
    {

        ILink<Guid, IUser<IUserData, IUserStatus, IInbox, IOutbox>> UserLink { get; }

        ILink<Guid, IUserHubService > UserHubLink { get; }
    }
}
