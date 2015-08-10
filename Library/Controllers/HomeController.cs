using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models.Entities;
using Library.Models.BusinessRules;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string bookname, string author, string category, string order)
        {
            //BookListRequest bookListRequest = new BookListRequest(bookname, author, category, Convert.ToInt32(order));
            BookListRequest bookListRequest = new BookListRequest(bookname, author, category, Convert.ToInt32(order));
            LibraryBusinessRules businessRules = new LibraryBusinessRules();
            BookList bookList = businessRules.GetBookList(bookListRequest);

            //ViewBag.Order = Convert.ToString(Convert.ToInt32(order), 2).PadLeft(3,'0');
            //ViewBag.Order = order;

            if (bookList != null)
            {
                return PartialView("PartialViews/BookList", bookList);
            }

            return View();
        }
    }
}