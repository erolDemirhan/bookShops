using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Entities
{
    public class BookShopsBooks
    {
        public int BookShopId { get; set; }
        public int BookId { get; set; }
        public BookShop BookShop { get; set; }
        public Book Book { get; set; }
    }
}
