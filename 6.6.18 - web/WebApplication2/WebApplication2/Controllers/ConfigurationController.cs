using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ConfigurationController : Controller
    {
        static Configuration configuration = new Configuration();        // GET: Configuration

        public ConfigurationController()
        {
            //configuration.Changed -= Changed;
            // configuration.Changed += Changed;
        }
        public ActionResult ConfigurationView()
        {
            return View(configuration);
        }

        public ActionResult Error(string handler)
        {
            ViewBag.name = handler;
            return View();
        }
    }
}