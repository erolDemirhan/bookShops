using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksAPI.DTOs;
using BooksAPI.Entities;
using BooksAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Controllers
{
    [ApiController]
    [Route("api/bookshops")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class BookShopsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public BookShopsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookShopDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.BookShops.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var entities = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<BookShopDTO>>(entities);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookShopDTO>> Get(int id)
        {
            var bookShop = await context.BookShops.FirstOrDefaultAsync(x => x.Id == id);

            if (bookShop == null)
            {
                return NotFound();
            }

            return mapper.Map<BookShopDTO>(bookShop);
        }

        [HttpPost]
        public async Task<ActionResult> Post(BookShopsCreationDTO bookCreationDTO)
        {
            var bookShop = mapper.Map<BookShop>(bookCreationDTO);
            context.Add(bookShop);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, BookShopCreationDTO movieCreationDTO)
        {
            var bookShop = await context.BookShops.FirstOrDefaultAsync(x => x.Id == id);

            if (bookShops == null)
            {
                return NotFound();
            }

            bookShops = mapper.Map(movieCreationDTO, bookShops);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var bookShop = await context.BookShops.FirstOrDefaultAsync(x => x.Id == id);

            if (bookShop == null)
            {
                return NotFound();
            }

            context.Remove(bookShop);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
