using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksAPI.DTOs;
using BooksAPI.Entities;
using BooksAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluent.Infrastructure.FluentModel;

namespace BooksAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly UserManager<IdentityUser> userManager;
        private string container = "books";

        public BooksController(ApplicationDbContext context, IMapper mapper,
            IFileStorageService fileStorageService, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
            this.userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<LandingPageDTO>> Get()
        {
            var top = 6;
            var today = DateTime.Today;

            var upcomingReleases = await context.Books
                .Where(x => x.ReleaseDate > today)
                .OrderBy(x => x.ReleaseDate)
                .Take(top)
                .ToListAsync();

            var inshops = await context.Books
                .Where(x => x.Inshops)
                .OrderBy(x => x.ReleaseDate)
                .Take(top)
                .ToListAsync();

            var landingPageDTO = new LandingPageDTO();
            landingPageDTO.UpcomingReleases = mapper.Map<List<BookDTO>>(upcomingReleases);
            landingPageDTO.Inshops = mapper.Map<List<BookDTO>>(inshops);
            return landingPageDTO;
        }

        [HttpGet("PostGet")]
        public async Task<ActionResult<BookPostGetDTO>> PostGet()
        {
            var bookShops = await context.BookShops.OrderBy(x => x.Name).ToListAsync();
            var genres = await context.Genres.OrderBy(x => x.Name).ToListAsync();

            var bookShopsDTO = mapper.Map<List<bookShopDTO>>(bookShops);
            var genresDTO = mapper.Map<List<GenreDTO>>(genres);

            return new BookPostGetDTO() { Genres = genresDTO, BookShops = bookShopsDTO };
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<BookDTO>> Get(int id)
        {
            var book = await context.Books
                .Include(x => x.BookGenres).ThenInclude(x => x.Genre)
                .Include(x => x.BookShopsBooks).ThenInclude(x => x.BookShop)
                .Include(x => x.BooksAuthors).ThenInclude(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var averageVote = 0.0;
            var userVote = 0;

            if (await context.Ratings.AnyAsync(x => x.BookId == id))
            {
                averageVote = await context.Ratings.Where(x => x.BookId == id)
                    .AverageAsync(x => x.Rate);

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
                    var user = await userManager.FindByEmailAsync(email);
                    var userId = user.Id;

                    var ratingDb = await context.Ratings.FirstOrDefaultAsync(x => x.BookId == id
                    && x.UserId == userId);

                    if (ratingDb != null)
                    {
                        userVote = ratingDb.Rate;
                    }
                }
            }

            var dto = mapper.Map<BookDTO>(book);
            dto.AverageVote = averageVote;
            dto.UserVote = userVote;
            dto.Authors = dto.Authors.OrderBy(x => x.Order).ToList();
            return dto;
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<List<BookDTO>>> Filter([FromQuery] FilterBooksDTO filterBooksDTO)
        {
            var booksQueryable = context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(filterBooksDTO.Title))
            {
                booksQueryable = booksQueryable.Where(x => x.Title.Contains(filterBooksDTO.Title));
            }

            if (filterBooksDTO.InShops)
            {
                booksQueryable = booksQueryable.Where(x => x.InShops);
            }

            if (filterBooksDTO.UpcomingReleases)
            {
                var today = DateTime.Today;
                booksQueryable = booksQueryable.Where(x => x.ReleaseDate > today);
            }

            if (filterBooksDTO.GenreId != 0)
            {
                booksQueryable = booksQueryable
                    .Where(x => x.BooksGenres.Select(y => y.GenreId)
                    .Contains(filterBooksDTO.GenreId));
            }

            await HttpContext.InsertParametersPaginationInHeader(booksQueryable);
            var books = await booksQueryable.OrderBy(x => x.Title).Paginate(filterBooksDTO.PaginationDTO)
                .ToListAsync();
            return mapper.Map<List<BookDTO>>(books);
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post([FromForm] BookCreationDTO bookCreationDTO)
        {
            var book = mapper.Map<Book>(bookCreationDTO);

            if (bookCreationDTO.Poster != null)
            {
                book.Poster = await fileStorageService.SaveFile(container, bookCreationDTO.Poster);
            }

            AnnotateAuthorsOrder(book);
            context.Add(book);
            await context.SaveChangesAsync();
            return book.Id;
        }

        [HttpGet("putget/{id:int}")]
        public async Task<ActionResult<BookPutGetDTO>> PutGet(int id)
        {
            var bookActionResult = await Get(id);
            if (bookActionResult.Result is NotFoundResult) { return NotFound(); }

            var book = bookActionResult.Value;

            var genresSelectedIds = book.Genres.Select(x => x.Id).ToList();
            var nonSelectedGenres = await context.Genres.Where(x => !genresSelectedIds.Contains(x.Id))
                .ToListAsync();

            var bookshopsIds = book.BookShops.Select(x => x.Id).ToList();
            var nonSelectedbookShops = await context.BookShops.Where(x =>
            !bookShopsIds.Contains(x.Id)).ToListAsync();

            var nonSelectedGenresDTOs = mapper.Map<List<GenreDTO>>(nonSelectedGenres);
            var nonSelectedBookShopsDTO = mapper.Map<List<BookShopDTO>>(nonSelectedBookShops);

            var response = new BookPutGetDTO();
            response.Book = book;
            response.SelectedGenres = book.Genres;
            response.NonSelectedGenres = nonSelectedGenresDTOs;
            response.SelectedBookshops = book.BookShops;
            response.NonSelectedBookShops = nonSelectedBookShopsDTO;
            response.Authors = book.Authors;
            return response;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] BookCreationDTO bookCreationDTO)
        {
            var book = await context.Books.Include(x => x.BooksAuthors)
                .Include(x => x.BooksGenres)
                .Include(x => x.BookshopsBooks)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            book = mapper.Map(bookCreationDTO, book);

            if (bookCreationDTO.Poster != null)
            {
                book.Poster = await fileStorageService.EditFile(container, bookCreationDTO.Poster,
                    book.Poster);
            }

            AnnotateAuthorsOrder(book);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private void AnnotateAuthorsOrder(Book book)
        {
            if (book.BooksAuthors != null)
            {
                for (int i = 0; i < book.BooksAuthors.Count; i++)
                {
                    book.BookAuthors[i].Order = i;
                }
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            context.Remove(book);
            await context.SaveChangesAsync();
            await fileStorageService.DeleteFile(book.Poster, container);
            return NoContent();
        }
    }
}
