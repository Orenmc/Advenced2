﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LogsController : Controller
    {
        static Logs LogsCollection = new Logs();        // GET: Configuration

        // GET: Logs
        public ActionResult LogsView()
        {
            return View(LogsCollection);
        }
    }
}