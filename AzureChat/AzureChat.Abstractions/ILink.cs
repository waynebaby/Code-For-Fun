using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AzureChat.Abstractions
{


    public interface ILink<TId,  TTarget> 
    {
        [DataMember]
        TId LinkId { get; set; }
        Task<TTarget>  GetItemAsync();
        TTarget GetItem();
        
        bool IsInstanceLoaded { get; }
        void ClearLoaded();
        Task  ClearLoadedAsync();
    }
}
