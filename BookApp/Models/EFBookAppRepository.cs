using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class EFBookAppRepository : IBookAppRepository
    {
        private BookstoreContext context { get; set; }
        public EFBookAppRepository(BookstoreContext temp)
        {
            context = temp;
        }
        public IQueryable<Book> Books => context.Books;
    }
}
