using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DenemeSWE.Controllers
{
    public class ShelfController : Controller
    {
        // GET: Shelf
        public ActionResult Index()
        {
            return View();
        }
    }
}