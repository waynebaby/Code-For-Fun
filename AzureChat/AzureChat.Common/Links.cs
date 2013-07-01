
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureChat.Common
{
    public interface ILink<TId, TTarget>
    {

        TId LinkId { get; set; }
        TTarget GetItem();
        Task<TTarget> GetItemAsync();

        TTarget RefreshIdAndGetItem(TId linkId);
        Task<TTarget> RefreshIdAndGetItemAsync(TId linkId);




        bool IsInstanceLoaded { get; }
        void ClearLoaded();
        Task ClearLoadedAsync();
    }

    public class Link<TId, TTarget> : ILink<TId, TTarget>
    {

        public Link(Func<TId, Task<TTarget>> factory)
        {

            _factory = factory;

        }
        Func<TId, Task<TTarget>> _factory;
        protected struct LoadedValue
        {

            /// <summary>
            /// 是否已经Load
            /// </summary>
            public bool IsLoaded { get; set; }
            /// <summary>
            /// 值
            /// </summary>
            public TTarget Value { get; set; }
            /// <summary>
            /// Link的Id
            /// </summary>
            public TId Id { get; set; }




        }

        protected LoadedValue _LoadedValue;
        /// <summary>
        /// 资源的Id
        /// </summary>
        [DataMember]
        public virtual TId LinkId
        {
            get
            {
                return _LoadedValue.Id;
            }
            set
            {

                if (!_LoadedValue.Id.Equals(value))
                {
                    _LoadedValue = new LoadedValue
                    {
                        Id = value
                    };
                }
            }
        }

        public virtual async Task<TTarget> GetItemAsync()
        {
            if (!_LoadedValue.IsLoaded)
            {
                return await RefreshIdAndGetItemAsync(_LoadedValue.Id);
            }
            else
            {

                return _LoadedValue.Value;
            }
        }

        public virtual async Task<TTarget> RefreshIdAndGetItemAsync(TId linkId)
        {

            _LoadedValue = new LoadedValue
            {
                Id = linkId,
                IsLoaded = true,
                Value = await _factory(linkId)

            };

            return _LoadedValue.Value;


        }

        public virtual TTarget GetItem()
        {
            return GetItemAsync().Result;
        }

        public virtual TTarget RefreshIdAndGetItem(TId linkId)
        {
            return RefreshIdAndGetItemAsync(linkId).Result;
        }

        public virtual bool IsInstanceLoaded
        {
            get { return _LoadedValue.IsLoaded; }
        }

        public virtual void ClearLoaded()
        {
            _LoadedValue = new LoadedValue() { Id = _LoadedValue.Id };
        }

        public virtual async Task ClearLoadedAsync()
        {
            _LoadedValue = new LoadedValue() { Id = _LoadedValue.Id };
            await Task.Yield();
        }
    }


    public class SafeLink<TId, TTarget> : Link<TId, TTarget>
    {
        public SafeLink(Func<TId, Task<TTarget>> factory)
            : base(factory)
        {
        }


        ConcurrentExclusiveSchedulerPair _CESP = new ConcurrentExclusiveSchedulerPair();

        public override void ClearLoaded()
        {
            var nt =
                Task.Factory.StartNew(
                    () => base.ClearLoaded(),
                    CancellationToken.None,
                     TaskCreationOptions.None,
                    _CESP.ExclusiveScheduler);
            nt.Wait();
        }
        public override async Task ClearLoadedAsync()
        {
            var nt =
                Task.Factory.StartNew<Task>(
                    ClearLoadedAsync,
                    CancellationToken.None,
                    TaskCreationOptions.None,
                    _CESP.ExclusiveScheduler);
            await nt;
        }

        public override TTarget GetItem()
        {
            var l = _LoadedValue;
            if (l.IsLoaded)
            {
                return l.Value;
            }
            var nt =
                Task.Factory.StartNew<Task<TTarget>>(
                    async () => await RefreshIdAndGetItemAsync(_LoadedValue.Id),
                    CancellationToken.None,
                    TaskCreationOptions.None,
                    _CESP.ExclusiveScheduler);
            return nt.Result.Result;
        }

        public override TTarget RefreshIdAndGetItem(TId linkId)
        {



            var nt =
                Task.Factory.StartNew<Task<TTarget>>(
                    async () => await RefreshIdAndGetItemAsync(linkId),
                    CancellationToken.None,
                    TaskCreationOptions.None,
                    _CESP.ExclusiveScheduler);

            return nt.Result.Result;
        }

        public override async Task<TTarget> RefreshIdAndGetItemAsync(TId linkId)
        {
            var nt =
               Task.Factory.StartNew<Task<TTarget>>(
                   async () => await base.RefreshIdAndGetItemAsync(linkId),
                   CancellationToken.None,
                   TaskCreationOptions.None,
                   _CESP.ExclusiveScheduler);

            return await await nt;
        }

        public override async Task<TTarget> GetItemAsync()
        {
            var l = _LoadedValue;
            if (l.IsLoaded)
            {
                return l.Value;
            }

            var nt =
          Task.Factory.StartNew<Task<TTarget>>(
              () => RefreshIdAndGetItemAsync(_LoadedValue.Id),
              CancellationToken.None,
              TaskCreationOptions.None,
              _CESP.ExclusiveScheduler);
            return await await nt;
        }

        public override bool IsInstanceLoaded
        {
            get
            {
                return base.IsInstanceLoaded;
            }
        }
        [DataMember]
        public override TId LinkId
        {
            get
            {
                var nt =
                Task.Factory.StartNew(
                    () => _LoadedValue.Id,
                    CancellationToken.None,
                     TaskCreationOptions.None,
                    _CESP.ConcurrentScheduler);
                return nt.Result;
            }
            set
            {
                var nt =
                 Task.Factory.StartNew(
                    () => _LoadedValue = new LoadedValue { Id = value },
                    CancellationToken.None,
                    TaskCreationOptions.None,
                    _CESP.ExclusiveScheduler);
                nt.Wait();
            }
        }
    }


    ///// <summary>
    ///// 一个根据ID取相关资源的类似LazyLoad的模式:Link
    ///// </summary>
    ///// <typeparam name="TId">Id类型</typeparam>
    ///// <typeparam name="TTarget">内容</typeparam>
    //[DataContract]
    //public abstract class LinkBase<TId, TTarget> : ILink<TId, TTarget>
    //{
    //    /// <summary>
    //    /// 构造函数k
    //    /// </summary>
    //    /// <param name="factory">传入Factory</param>
    //    public LinkBase(Func<TId, Task<TTarget>> factory)
    //    {
    //        _Factory = factory;
    //    }



    //    /// <summary>
    //    /// 由于是否装载，Id与取到的值 往往是对应的，所以用一个struct进行状态维护
    //    /// </summary>

    //    protected class LoadedValue
    //    {
    //        /// <summary>
    //        /// 是否已经Load
    //        /// </summary>
    //        public bool IsLoaded { get; set; }
    //        /// <summary>
    //        /// 值
    //        /// </summary>
    //        public TTarget Value { get; set; }
    //        /// <summary>
    //        /// Link的Id
    //        /// </summary>
    //        public TId Id { get; set; }

    //        public override bool Equals(object obj)
    //        {
    //            return Id.Equals(((LoadedValue)obj).Id);
    //        }

    //        public override int GetHashCode()
    //        {
    //            return Id.GetHashCode();
    //        }
    //    }

    //    protected LoadedValue _LoadedValue = new LoadedValue();

    //    /// <summary>
    //    /// 定位资源用的工厂
    //    /// </summary>
    //    protected Func<TId, Task<TTarget>> _Factory;


    //    /// <summary>
    //    /// 资源的Id
    //    /// </summary>
    //    [DataMember]
    //    public virtual TId LinkId
    //    {
    //        get
    //        {
    //            return _LoadedValue.Id;
    //        }
    //        set
    //        {

    //            if (!_LoadedValue.Id.Equals(value))
    //            {
    //                _LoadedValue = new LoadedValue
    //                {
    //                    Id = value
    //                };
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 异步取得资源
    //    /// </summary>
    //    /// <returns>资源内容</returns>
    //    public async Task<TTarget> GetItemAsync()
    //    {
    //        return (await internalGetValueAsync()).Value;
    //    }

    //    /// <summary>
    //    /// 同步取得资源
    //    /// </summary>
    //    /// <returns>资源内容</returns>
    //    public TTarget GetItem()
    //    {

    //        return (internalGetValue()).Value;
    //    }

    //    /// <summary>
    //    /// 资源是否载入
    //    /// </summary>
    //    public bool IsInstanceLoaded
    //    {
    //        get { return _LoadedValue.IsLoaded; }

    //    }

    //    /// <summary>
    //    /// 实际同步取得值的逻辑
    //    /// </summary>
    //    /// <param name="linkid">资源Id</param>
    //    /// <returns>资源值</returns>
    //    protected abstract LoadedValue internalGetValue();

    //    /// <summary>
    //    /// 实际异步取得值的逻辑
    //    /// </summary>
    //    /// <param name="linkid">资源Id</param>
    //    /// <returns>资源值</returns>
    //    protected abstract Task<LoadedValue> internalGetValueAsync();


    //    /// <summary>
    //    /// 清空
    //    /// </summary>
    //    public virtual void ClearLoaded()
    //    {
    //        _LoadedValue = new LoadedValue() { Id = _LoadedValue.Id };

    //    }



    //    public virtual async Task ClearLoadedAsync()
    //    {
    //        ClearLoaded();
    //        await Task.Yield();
    //    }


    //    public virtual async Task<TTarget> RefreshIdAndGetItemAsync(TId linkId)
    //    {
    //        LinkId = linkId;
    //        return (await internalGetValueAsync()).Value;
    //    }

    //    public virtual TTarget RefreshIdAndGetItem(TId linkId)
    //    {
    //        LinkId = linkId;
    //        return internalGetValue().Value;
    //    }
    //}

    ///// <summary>
    ///// 不考虑线程安全的Link
    ///// </summary>
    ///// <typeparam name="TId">资源Id类型</typeparam>
    ///// <typeparam name="TTarget">资源类型</typeparam>
    //public class SimpleLink<TId, TTarget> : LinkBase<TId, TTarget>
    //{

    //    public SimpleLink(Func<TId, Task<TTarget>> factory)
    //        : base(factory)
    //    {

    //    }
    //    protected override LinkBase<TId, TTarget>.LoadedValue internalGetValue()
    //    {

    //        return internalGetValueAsync().Result;
    //    }

    //    protected override async Task<LinkBase<TId, TTarget>.LoadedValue> internalGetValueAsync()
    //    {

    //        var tLoaded = _LoadedValue;

    //        if (!tLoaded.IsLoaded)
    //        {
    //            //简单的调用factory等待值 
    //            _LoadedValue = new LoadedValue
    //             {
    //                 Id = tLoaded.Id,
    //                 IsLoaded = true,
    //                 Value = await _Factory(tLoaded.Id),
    //             };

    //            return _LoadedValue;
    //        }
    //        else
    //        {
    //            return tLoaded;

    //        }

    //    }
    //}
    ///// <summary>
    ///// 线程安全的Link
    ///// </summary>
    ///// <typeparam name="TId">资源Id类型</typeparam>
    ///// <typeparam name="TTarget">资源内容类型</typeparam>
    //public class SafeLink<TId, TTarget> : SimpleLink<TId, TTarget>
    //{
    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="factory">传入Factory</param>
    //    public SafeLink(Func<TId, Task<TTarget>> factory)
    //        : base(factory)
    //    {

    //    }

    //    System.Threading.Tasks.ConcurrentExclusiveSchedulerPair rws = new ConcurrentExclusiveSchedulerPair();

    //    protected override LinkBase<TId, TTarget>.LoadedValue internalGetValue()
    //    {
    //        return internalGetValueAsync().Result;
    //    }

    //    protected override async Task<LinkBase<TId, TTarget>.LoadedValue> internalGetValueAsync()
    //    {
    //        var nt = Task.Factory.StartNew<Task<LoadedValue>>
    //            (
    //                base.internalGetValueAsync,
    //                CancellationToken.None,
    //                TaskCreationOptions.None,
    //                rws.ExclusiveScheduler
    //            );//所有的Task用互斥的编排器

    //        return await (await nt);
    //    }



    //    public override void ClearLoaded()
    //    {
    //        ClearLoadedAsync().Wait();
    //    }

    //    public override async Task ClearLoadedAsync()
    //    {
    //        var nt = Task.Factory.StartNew<Task>
    //        (
    //            base.ClearLoadedAsync,
    //            CancellationToken.None,
    //            TaskCreationOptions.None,
    //            rws.ExclusiveScheduler
    //        );
    //        await (await nt);
    //    }
    //    //[DataMember]
    //    //public override TId LinkId
    //    //{
    //    //    get
    //    //    {
    //    //        return base.LinkId;
    //    //    }
    //    //    set
    //    //    {
    //    //        var nt = Task.Factory.StartNew
    //    //          (
    //    //               () => base.LinkId = value,
    //    //               CancellationToken.None,
    //    //               TaskCreationOptions.None,
    //    //               rws.ExclusiveScheduler
    //    //          );

    //    //        nt.Wait();
    //    //    }
    //    //}
    //    public override TTarget RefreshIdAndGetItem(TId linkId)
    //    {
    //        return RefreshIdAndGetItemAsync(linkId).Result;
    //    }



    //    public override async Task<TTarget> RefreshIdAndGetItemAsync(TId linkId)
    //    {
    //        var nt = Task.Factory.StartNew<Task<TTarget>>
    //            (
    //                () => base.RefreshIdAndGetItemAsync(linkId),
    //                CancellationToken.None,
    //                TaskCreationOptions.None,
    //                rws.ExclusiveScheduler
    //            );//所有的Task用互斥的编排器

    //        return (await (await nt));
    //    }
    //}
}
