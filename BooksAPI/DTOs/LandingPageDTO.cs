using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.DTOs
{
    public class LandingPageDTO
    {
        public List<BookDTO> InShops { get; set; }
        public List<BookDTO> UpcomingBooks { get; set; }
    }
}
