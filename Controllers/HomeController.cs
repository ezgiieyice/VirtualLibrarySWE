﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DenemeSWE.Models;

namespace DenemeSWE.Controllers
{
    public class HomeController : Controller
    {
        IheBookEntities1 db = new IheBookEntities1();
        public ActionResult Index()
        {
            return View();
        }


    }
}