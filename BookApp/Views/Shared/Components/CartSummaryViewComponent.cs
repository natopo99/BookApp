using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Views.Shared.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Basket bas;
        public CartSummaryViewComponent(Basket cartService)
        {
            bas = cartService;
        }
        public IViewComponentResult Invoke()
        {
            return View(bas);
        }
    }
}
