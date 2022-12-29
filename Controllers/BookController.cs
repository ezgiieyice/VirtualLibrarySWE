using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using DenemeSWE.Models;

namespace DenemeSWE.Controllers
{
    public class BookController : Controller
    {
        IheBookEntities5 db = new IheBookEntities5();

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
        public ActionResult BookDetail(string id)
        {
            if (Session["name"] != null)
            {
                var book = db.Books.Where(x => x.book_id == id).FirstOrDefault();
                return View(book);
            }
            return View();
        }
        [HttpGet]
        public ActionResult AddBook()
        {
            if (Session["name"] != null)
            {
                return View();
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddBook(string bookId)
        {
            if (Session["id"] != null)
            {
                var id = Convert.ToInt32(Session["id"]);
                var userBook = db.Shelf.Where(x => x.user_id == id && x.book_id == bookId).FirstOrDefault();
                if(userBook != null)
                {
                    ViewData["Error"] = "Your already have this book!";
                }
                else
                {
                    var book = db.Books.Where(x => x.book_id == bookId).FirstOrDefault();
                    var shelf = new Shelf();
                    shelf.user_id = id;
                    shelf.author = book.author;
                    shelf.subject = book.subject;
                    shelf.title = book.title;
                    shelf.release_date = book.release_date;
                    shelf.book_id = book.book_id;
                    shelf.photo = book.photo;
                    shelf.language = book.language;
                    shelf.category = book.category;
                    shelf.reading_link = book.reading_link;
                    db.Shelf.Add(shelf);
                    db.SaveChanges();
                }
               
                return RedirectToAction("BookList");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public ActionResult ReadBook(string bookId)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            int userId = Convert.ToInt32(Session["id"]);
            var book = db.Books.Where(x => x.book_id == bookId).FirstOrDefault();
            
            if(book != null)
            {
                ViewBag.BookId = bookId;
                var userBook = db.Shelf.Where(x => x.user_id == userId && x.book_id == bookId).FirstOrDefault();
                ViewBag.ScrollPosition = userBook.scrollPostion;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(book.reading_link);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var html = reader.ReadToEnd();
                    html = html.Replace("images/", "https://www.gutenberg.org/cache/epub/69647/images/");
                    ViewBag.Result = html;
                }
            }
          
            return View();
        }

        [HttpPost]
        public ActionResult SaveLocation(int userId, string bookId, float scrollPostion)
        {
            var userBook = db.Shelf.Where(x => x.user_id == userId && x.book_id == bookId).FirstOrDefault();
            if(userBook != null)
            {
                userBook.scrollPostion = scrollPostion;
                db.Shelf.AddOrUpdate(userBook);
                db.SaveChanges();
            }
            return Content("");
        }
    }

    /*[HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddBook(Books b)
    {

        if (ModelState.IsValid)
        {

            /*var book = db.Books.FirstOrDefault(a => a.book_id == b.book_id);
            //var bo = db.Books.Find(b.book_id);
            db.Shelf.Add(new Shelf
            {
                book_id = book.book_id,
                title = book.title,
                author = book.author,
                category = book.category,
                language = book.language,
                photo = book.photo,
                reading_link = null,
                release_date = book.release_date,
                subject = book.subject

            });

            db.SaveChanges();

            return RedirectToAction("MyShelf","Shelf");*/

    /*if (check == null)
    {
        db.Books.Add(book);
        db.SaveChanges();
        return RedirectToAction("BookList");
    }
    else
    {
        ModelState.AddModelError("", "This book is already in the library");
    }

    if (check == null)
    {

            //user.password = GetMD5(user.password);
            db.Configuration.ValidateOnSaveEnabled = false;
            db.Shelf.Add(shelf);
            db.SaveChanges();
            return RedirectToAction("BookList");
    }
    else
    {
        ViewData["AddFlag"] = "You already have the book!!";
        return View();
    }

}
return View();
}
return RedirectToAction("BookList");
}
public ActionResult RemoveBook()
{
return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult RemoveBook(User user)
{

if (ModelState.IsValid)
{
    var check = db.User.FirstOrDefault(s => s.email == user.email);
    if (check == null)
    {
           //user.password = GetMD5(user.password);
            db.Configuration.ValidateOnSaveEnabled = false;
            db.User.Add(user);
            db.SaveChanges();
            return RedirectToAction("MyShelf");

    }
    else
    {
        ViewData["SignupFlag"] = "Email already exists!!";
        return View();
    }

}
return View();
}
   
}
     */
}