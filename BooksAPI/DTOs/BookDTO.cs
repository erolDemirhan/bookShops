using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InShops { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        public List<GenreDTO> Genres { get; set; }
        public List<BookShopsDTO> BookShops { get; set; }
        public List<AuthorsMovieDTO> Authors { get; set; }
        public double AverageVote { get; set; }
        public int UserVote { get; set; }
    }
}
