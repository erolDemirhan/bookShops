using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.DTOs
{
    public class Rating
    {
        [Range(1, 5)]
        public int Rating { get; set; }
        public int BookId { get; set; }
    }
}
