using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DenemeSWE.Models;

namespace DenemeSWE.Controllers
{
    public class BookController : Controller
    {
        IheBookEntities1 db = new IheBookEntities1();
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BookList()
        {
            if (Session["name"] != null)
            {
                    var books = db.Books.ToList();
                    return View(books);
                
            }
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> BookList(string search)
        {
            
            ViewData["Books"] = search;
            var query = from x in db.Books select x;
            if (!String.IsNullOrEmpty(search))
            {
                if (search.Length >= 3)
                {
                    query = query.Where(x => x.title.Contains(search));
                }
                else
                {
                    ViewData["LoginFlag"] = "You must enter at least 3 characters to search!!";
                    return View(await query.AsNoTracking().ToListAsync());
                }
            }
            return View(await query.AsNoTracking().ToListAsync());
        }
    }
}