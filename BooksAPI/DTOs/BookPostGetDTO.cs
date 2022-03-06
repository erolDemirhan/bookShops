using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.DTOs
{
    public class BookPostGetDTO
    {
        public List<GenreDTO> Genres { get; set; }
        public List<BookShopsDTO> BookShops { get; set; }
    }
}
