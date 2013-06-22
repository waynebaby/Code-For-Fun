using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureChat.Abstractions
{
    public interface IUserHub : IObservable<IMessage<object, IMessageControlData>>
    {
        IPostOffice PostOffice
        {
            get;
            set;
        }

        IEnumerable<IUser<IUserData, IUserStatus, IInbox, IOutbox>> GetOnlineUsersSnapshot();

        Task<IUser<IUserData, IUserStatus, IInbox, IOutbox>> LogOnOrLocateUser(ILoginToken<object> token);

        Task LogOffUser(IUser<IUserData, IUserStatus, IInbox, IOutbox> user);



    }
}
