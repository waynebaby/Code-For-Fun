using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureChat.Abstractions
{
    public interface IUserHubService : IObservable<IMessage<object, IMessageControlData>>
    {
        ILink<Guid, IPostOfficeService> PostOfficeLink
        {
            get;
            set;
        }

        Task<IEnumerable<IUser<IUserData, IUserStatus, IInbox, IOutbox>>> GetLocalUsersSnapshotAsync();

        Task<IUser<IUserData, IUserStatus, IInbox, IOutbox>> LogOnOrLocateUserAsync(ILoginToken<object> token);

        Task LogOffUserAsync(IUser<IUserData, IUserStatus, IInbox, IOutbox> user);



    }
}
