using BookApp.Models;
using BookApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Controllers
{
    public class HomeController : Controller
    {
        private IBookAppRepository repo; 

        public HomeController(IBookAppRepository bar)
        {
            repo = bar;
        }



        public IActionResult Index(string category, int pageNum = 1)
        {
            int pageSize = 10; //specify the page size 

            var bvm = new BooksViewModel
            {
                Books = repo.Books
                .Where(c => c.Category == category || category == null )
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize) // these run like SQL
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = (category == null ? repo.Books.Count() : repo.Books.Where(bvm => bvm.Category == category).Count()) , // get total number of books
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum


                }
            };

           

            return View(bvm);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
