using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SignalR.Hubs;

namespace SignalR.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var ChatHub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>(); //Controllerdan server gibi mesaj gönderme
            ChatHub.Clients.All.GetMessageOther("message");


            return View();
        }
    }
}