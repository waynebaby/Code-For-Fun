using System;
using System.Reactive;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AzureChat.Models.Test
{
    [TestClass]
    public class PostOfficeTest
    {
        [TestMethod]
        public async void PostOfficeOnlineSubscription()
        {
            PostOffice p = new PostOffice(Guid.NewGuid());
           
            var task=new Task(()=>{});

            




        }
    }
}
