using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AzureChat.Abstractions
{
 
    public interface IPostOfficeService  :
        IDisposable ,
        IObserver<ServiceRegisterNotifyMessage<IUser<IUserData, IUserStatus, IInbox, IOutbox>>>,
        IObserver<ServiceRegisterNotifyMessage<IChannel<IChannelData, IChannelStatus>>>,
        IObserver<ServiceRegisterNotifyMessage<IPostOfficeService>>,
        IObservable<ServiceRegisterNotifyMessage<IUser<IUserData, IUserStatus, IInbox, IOutbox>>>,
        IObservable<ServiceRegisterNotifyMessage<IChannel<IChannelData, IChannelStatus>>>,
        IObservable<ServiceRegisterNotifyMessage<IPostOfficeService>>
    {
        Guid Id { get; }

        //Task<IPostOfficeService> LocateUserHubServiceAsync(Guid id);
        //Task<IChannelHubService> LocateChannelHubServiceAsync(Guid id);
        //Task<IPostOfficeService> LocatePostOfficeServiceAsync(Guid id);
 
        Task<IUser<IUserData, IUserStatus, IInbox, IOutbox>> LocateUserAsync(Guid id);
        Task<IChannel<IChannelData, IChannelStatus>> LocateChannelAsync(Guid id);
        IUserHubService LocalUserHubService { get; }
        IChannelHubService LocalChannelHubService { get; }
        bool IsRemote { get; }

    }





}
