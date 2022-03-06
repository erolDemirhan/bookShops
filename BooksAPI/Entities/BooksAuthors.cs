using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Entities
{
    public class BooksAuthors
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        [StringLength(maximumLength: 75)]
        public string Character { get; set; }
        public int Order { get; set; }
        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
