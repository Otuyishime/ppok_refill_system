using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ppok_refill.Models;

namespace Web
{
    [HubName(hubName:"DuePickUps")]
    public class DuePickUpsHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void getDuePickUp()
        {
            var pendingPickUps = new PickUpsDBManager();
            int _duepickups = pendingPickUps.getDuePickUps().Count;

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<DuePickUpsHub>();
            context.Clients.All.onGetDuePickUp(_duepickups);
        }
    }
}