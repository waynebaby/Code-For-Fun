using AzureChat.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureChat.Common
{
    public class Link<TId, TTarget> : ILink<TId, TTarget> 
    {
        TId _LinkId;
        public TId LinkId
        {
            get
            {
                return _LinkId;
            }
            set
            {
                _LinkId = value;
            }
        }

        public Task<TTarget> GetItemAsync()
        {
           
        }

        public TTarget GetItem()
        {
            throw new NotImplementedException();
        }

        public bool IsInstanceLoaded
        {
            get { throw new NotImplementedException(); }
        }

        public void ClearLoaded()
        {
            throw new NotImplementedException();
        }
    }
}
