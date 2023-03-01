using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public interface IBookAppRepository
    {

        IQueryable<Book> Books { get; }
    }
}
