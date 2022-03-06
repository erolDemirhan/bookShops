using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.DTOs
{
    public class BookPutGetDTO
    {
        public BookDTO Book { get; set; }
        public List<GenreDTO> SelectedGenres { get; set; }
        public List<GenreDTO> NonSelectedGenres { get; set; }
        public List<BookShopDTO> SelectedBookShops { get; set; }
        public List<BookShopDTO> NonSelectedBookShops { get; set; }
        public List<AuthorsBookDTO> Authors { get; set; }
    }
}
