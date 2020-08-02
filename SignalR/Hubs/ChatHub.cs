using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public void SendMessage(string name, string message)
        {   
            Clients.Others.GetMessageOther(name, message); //Yazan client dışındakilere gider

            Clients.Caller.GetMessageCaller(message); //sadece yazana client e gider


            /*Clients.All.Metod();
              Clients.user("id").Metod();
              Clients.cliend(id).Metod();
              Clients.users();
              Clients.clients();

             group.add();
             group.remove();
             Clients.groups();
             Clients.group();
             */


        }
        
        public override Task OnConnected()
        {
            string id = Context.ConnectionId;
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}
 
 
 
 
 
 
 
 