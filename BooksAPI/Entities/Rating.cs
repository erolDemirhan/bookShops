using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
