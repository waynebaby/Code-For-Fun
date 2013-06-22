using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureChat.Abstractions
{
    public interface IUserData
    {
        Guid  UserId
        {
            get;
            set;
        }
    }
}
