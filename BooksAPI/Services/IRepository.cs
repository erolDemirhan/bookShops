using BooksAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Services
{
    public interface IRepository
    {
        void AddGenre(Genre genre);
        Task<List<Genre>> GetAllGenres();
        Genre GetGenreById(int Id);
    }
}
