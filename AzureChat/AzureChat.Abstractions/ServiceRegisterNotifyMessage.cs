using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AzureChat.Abstractions
{
    [DataContract]
    public struct ServiceRegisterNotifyMessage<TService>
    {
        [DataMember ]
        public TService Service { get; set; }
        [DataMember]
        public ChangeNotifyType NotifyType { get; set; }
    }
}
