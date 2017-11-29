using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HapoalimClientSideAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ValuesController VC = new Controllers.ValuesController();
            var userUniqueToken = VC.IsAlive();

            return View();
        }
    }
}
