using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookApp.Models;
using BookApp.Models.Infrastructure;

namespace BookApp.Pages
{
    public class CartModel : PageModel
    {
        private IBookAppRepository repo { get; set; }
        public CartModel (IBookAppRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }
        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }
        public IActionResult OnPost(int bookid, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookID == bookid); //

            basket.AddItem(b, 1);


            return RedirectToPage(new { ReturnUrl = returnUrl});
        }

        public IActionResult OnPostRemove(int bkID, string returnURL)
        {
            basket.RemoveItem(basket.Items.First(x => x.Book.BookID == bkID).Book);

            return RedirectToPage( new {ReturnUrl = returnURL});
        }
    }
}
