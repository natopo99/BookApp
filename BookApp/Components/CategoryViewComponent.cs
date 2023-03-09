using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookApp.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private IBookAppRepository repo { get; set;  }

        public CategoryViewComponent(IBookAppRepository temp)
        {
            repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            var categories = repo.Books
                .Select(X => X.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(categories);
        }
    }
}
