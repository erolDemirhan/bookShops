using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Entities
{
    public class Books
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 75)]
        [Required]
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InShops { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        public List<BooksGenres> BooksGenres { get; set; }
        public List<BookShopsBooks> BookShopsBooks { get; set; }
        public List<BookAuthors> BookAuthors { get; set; }
    }
}
