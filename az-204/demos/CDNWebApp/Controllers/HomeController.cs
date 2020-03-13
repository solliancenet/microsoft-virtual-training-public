using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CDNWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //YOU WILL GET BACK THE REFERENCE STRING
            string val1 = ConfigurationManager.AppSettings["KeyVaultValue"];

            string val2 = Environment.GetEnvironmentVariable("KeyVaultValue");

            ViewBag.KeyVaultValue1 = val1;
            ViewBag.KeyVaultValue2 = val1;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}