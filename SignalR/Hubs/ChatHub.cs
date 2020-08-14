using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;

namespace SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public static ConcurrentDictionary<string, string> ConnectedUsers = new ConcurrentDictionary<string, string>(); //Bağlı kullanıcıların bilgisi tutulur

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

        public void SpesificMessage(string message, string name)
        {
            var userid = ConnectedUsers.Where(i => i.Value == name).FirstOrDefault(); //İsime bağlı user bulunuyor

            Clients.Client(userid.Key).GetMessageOther(message); //mesaj gönderiliyor
        }


        public override Task OnConnected() //Her kullanıcı bağlandığında çalışır
        {
            Clients.Caller.GetMessageCaller("Hoşgeldin");
            string id = Context.ConnectionId;

            return base.OnConnected();
        }

        public void Join(string name)
        {
            ConnectedUsers.AddOrUpdate(Context.ConnectionId, name, (k, v) => name); //kullanıcı eklenir
        }


        public void JoinGroup(string Name) //Gruba katılma
        {
            Groups.Add(Context.ConnectionId, Name);
        }

        public void LeaveGroup(string Name) //Gruptan çıkma
        {
            Groups.Remove(Context.ConnectionId, Name);
        }

        public void MessageGroup(string message, string Name) //gruba mesaj atma
        {
            Clients.Group(Name).GetMessageOther(message);
        }




        public override Task OnDisconnected(bool stopCalled)
        {
            var name = string.Empty;
            ConnectedUsers.TryRemove(Context.ConnectionId, out name); //kullanıcı silinir

            return base.OnDisconnected(stopCalled);
        }
    }
}
 
 
 
 
 
 
 
 