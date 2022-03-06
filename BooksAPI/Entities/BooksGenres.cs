using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Entities
{
    public class BooksGenres
    {
        public int GenreId { get; set; }
        public int BookId { get; set; }
        public Genre Genre { get; set; }
        public Book Book { get; set; }
    }
}
