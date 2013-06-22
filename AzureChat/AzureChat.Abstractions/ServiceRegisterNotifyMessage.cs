using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public struct ServiceRegisterNotifyMessage<TService>
    {
        public TService Service { get; set; }
        public ChangeNotifyType NotifyType { get; set; }
    }
}
