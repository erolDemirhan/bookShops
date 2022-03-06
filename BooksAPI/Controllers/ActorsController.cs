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
    [Route("api/authors")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly string containerName = "authors";

        public AuthorsController(ApplicationDbContext context, IMapper mapper,
            IFileStorageService fileStorageService)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.Authors.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var authors = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<AuthorDTO>>(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDTO>> Get(int id)
        {
            var author = await context.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return mapper.Map<AuthorDTO>(author);
        }

        [HttpGet("searchByName/{query}")]
        public async Task<ActionResult<List<AuthorsMovieDTO>>> SearchByName(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) { return new List<AuthorsMovieDTO>(); }

            return await context.Authors
                .Where(x => x.Name.Contains(query))
                .OrderBy(x => x.Name)
                .Select(x => new AuthorsMovieDTO { Id = x.Id, Name = x.Name, Picture = x.Picture })
                .Take(5)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] AuthorCreationDTO authorCreationDTO)
        {
            var author = mapper.Map<Author>(authorCreationDTO);

            if (authorCreationDTO.Picture != null)
            {
                author.Picture = await fileStorageService.SaveFile(containerName, authorCreationDTO.Picture);
            }

            context.Add(author);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] AuthorCreationDTO authorCreationDTO)
        {
            var author = await context.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            author = mapper.Map(authorCreationDTO, author);

            if (authorCreationDTO.Picture != null)
            {
                author.Picture = await fileStorageService.EditFile(containerName,
                        authorCreationDTO.Picture, author.Picture);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var author = await context.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            context.Remove(author);
            await context.SaveChangesAsync();
            await fileStorageService.DeleteFile(author.Picture, containerName);
            return NoContent();
        }
    }
}
