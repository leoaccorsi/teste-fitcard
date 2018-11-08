using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestePraticoModel.Enum;

namespace TestePratico.Controllers
{
    public class BaseController : Controller
    {
        public void Alert(string message, NotificationType notificationType)
        {
            var msg = "swal('Alerta', '" + message + "','" + notificationType + "')";
            TempData["notification"] = msg;
        }
    }
}