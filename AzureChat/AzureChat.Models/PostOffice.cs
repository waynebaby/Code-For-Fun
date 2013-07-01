using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AzureChat.Abstractions;
using System.Reactive;
using System.Threading.Tasks.Dataflow;
namespace AzureChat.Models
{

    [DataContract(IsReference = true)]
    public class PostOffice : IPostOfficeService
    {



        public PostOffice(Guid id)
        {
            _Id = id;
   
        }

        #region Observer and Observables core
        WriteOnceBlock<ServiceRegisterNotifyMessage<IPostOfficeService>> _officeBlock
            = new WriteOnceBlock<ServiceRegisterNotifyMessage<IPostOfficeService>>(x => x);

        #endregion


        [DataMember(Name = "Id")]
        Guid _Id;
        public Guid Id
        {
            get
            {
                return _Id;
            }

        }


        public Task<IUser<IUserData, IUserStatus, IInbox, IOutbox>> LocateUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IChannel<IChannelData, IChannelStatus>> LocateChannelAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IUserHubService LocalUserHubService
        {
            get { throw new NotImplementedException(); }
        }

        public IChannelHubService LocalChannelHubService
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsRemote
        {
            get { return false; }
        }


        #region IObserver<ServiceRegisterNotifyMessage<IUser<IUserData,IUserStatus,IInbox,IOutbox>>> Members

        void IObserver<ServiceRegisterNotifyMessage<IUser<IUserData, IUserStatus, IInbox, IOutbox>>>.OnCompleted()
        {

        }

        void IObserver<ServiceRegisterNotifyMessage<IUser<IUserData, IUserStatus, IInbox, IOutbox>>>.OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        void IObserver<ServiceRegisterNotifyMessage<IUser<IUserData, IUserStatus, IInbox, IOutbox>>>.OnNext(ServiceRegisterNotifyMessage<IUser<IUserData, IUserStatus, IInbox, IOutbox>> value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IObserver<ServiceRegisterNotifyMessage<IChannel<IChannelData,IChannelStatus>>> Members

        void IObserver<ServiceRegisterNotifyMessage<IChannel<IChannelData, IChannelStatus>>>.OnCompleted()
        {
            throw new NotImplementedException();
        }

        void IObserver<ServiceRegisterNotifyMessage<IChannel<IChannelData, IChannelStatus>>>.OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        void IObserver<ServiceRegisterNotifyMessage<IChannel<IChannelData, IChannelStatus>>>.OnNext(ServiceRegisterNotifyMessage<IChannel<IChannelData, IChannelStatus>> value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IObserver<ServiceRegisterNotifyMessage<IPostOfficeService>> Members

        void IObserver<ServiceRegisterNotifyMessage<IPostOfficeService>>.OnCompleted()
        {
            throw new NotImplementedException();
        }

        void IObserver<ServiceRegisterNotifyMessage<IPostOfficeService>>.OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        void IObserver<ServiceRegisterNotifyMessage<IPostOfficeService>>.OnNext(ServiceRegisterNotifyMessage<IPostOfficeService> value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IObservable<ServiceRegisterNotifyMessage<IUser<IUserData,IUserStatus,IInbox,IOutbox>>> Members

        IDisposable IObservable<ServiceRegisterNotifyMessage<IUser<IUserData, IUserStatus, IInbox, IOutbox>>>.Subscribe(IObserver<ServiceRegisterNotifyMessage<IUser<IUserData, IUserStatus, IInbox, IOutbox>>> observer)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IObservable<ServiceRegisterNotifyMessage<IChannel<IChannelData,IChannelStatus>>> Members

        IDisposable IObservable<ServiceRegisterNotifyMessage<IChannel<IChannelData, IChannelStatus>>>.Subscribe(IObserver<ServiceRegisterNotifyMessage<IChannel<IChannelData, IChannelStatus>>> observer)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IObservable<ServiceRegisterNotifyMessage<IPostOfficeService>> Members

        IDisposable IObservable<ServiceRegisterNotifyMessage<IPostOfficeService>>.Subscribe(IObserver<ServiceRegisterNotifyMessage<IPostOfficeService>> observer)
        {
            observer.OnNext(new ServiceRegisterNotifyMessage<IPostOfficeService>() { NotifyType = ChangeNotifyType.Online, Service = this });
            return _officeBlock.AsObservable().Subscribe(observer);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
          _officeBlock.Post  (new ServiceRegisterNotifyMessage<IPostOfficeService>() { NotifyType = ChangeNotifyType.Online, Service = this });
        }

        #endregion
    }
}
