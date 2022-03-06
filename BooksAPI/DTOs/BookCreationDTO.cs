using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.DTOs
{
    public class BookCreationDTO
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InShops { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IFormFile Poster { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenresIds { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> BookShopsIds { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<BookShopsCreationDTO>>))]
        public List<BooksAuthorsCreationDTO> Authors { get; set; }

    }
}
