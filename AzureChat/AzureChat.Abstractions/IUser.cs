using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public interface IUser<out TUserData, out TUserStatus, out TInbox, out TOutbox>  
        where TUserData : IUserData
        where TUserStatus : IUserStatus
        where TInbox : IInbox
        where TOutbox : IOutbox
    {
        Guid Id
        {
            get;
        }

        TUserData Data
        {
            get;

        }

        TUserStatus Status
        {
            get;

        }

        TInbox Inbox
        {
            get;

        }

        TOutbox Outbox
        {
            get;

        }

        bool IsRemote { get; }
    }
}
