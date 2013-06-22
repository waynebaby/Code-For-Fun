using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public interface IUserStatus
    {
        Guid UserId
        {
            get;
            set;
        }

        IUserHub UserHub
        {
            get;
            set;
        }
    }
}
