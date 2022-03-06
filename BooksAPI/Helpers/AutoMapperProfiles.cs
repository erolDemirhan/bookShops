using AutoMapper;
using Microsoft.AspNetCore.Identity;
using BooksAPI.DTOs;
using BooksAPI.Entities;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BooksAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFAuthory geometryFAuthory)
        {
            CreateMap<GenreDTO, Genre>().ReverseMap();
            CreateMap<GenreCreationDTO, Genre>();

            CreateMap<AuthorDTO, Author>().ReverseMap();
            CreateMap<AuthorCreationDTO, Author>()
                .ForMember(x => x.Picture, options => options.Ignore());

            CreateMap<Bookshop, BookshopDTO>()
               .ForMember(x => x.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
               .ForMember(x => x.Longitude, dto => dto.MapFrom(prop => prop.Location.X));

            CreateMap<BookshopCreationDTO, Bookshop>()
                .ForMember(x => x.Location, x => x.MapFrom(dto =>
                geometryFAuthory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));

            CreateMap<BookCreationDTO, Book>()
               .ForMember(x => x.Poster, options => options.Ignore())
               .ForMember(x => x.BooksGenres, options => options.MapFrom(MapBooksGenres))
               .ForMember(x => x.BookshopsBooks, options => options.MapFrom(MapBookshopsBooks))
               .ForMember(x => x.BooksAuthors, options => options.MapFrom(MapBooksAuthors));

            CreateMap<Book, BookDTO>()
               .ForMember(x => x.Genres, options => options.MapFrom(MapBooksGenres))
               .ForMember(x => x.Bookshops, options => options.MapFrom(MapBookshopsBooks))
               .ForMember(x => x.Authors, options => options.MapFrom(MapBooksAuthors));

            CreateMap<IdentityUser, UserDTO>();
        }

        private List<AuthorsBookDTO> MapBooksAuthors(Book Book, BookDTO BookDTO)
        {
            var result = new List<AuthorsBookDTO>();

            if (Book.BooksAuthors != null)
            {
                foreach (var BooksAuthors in Book.BooksAuthors)
                {
                    result.Add(new AuthorsBookDTO()
                    {
                        Id = BooksAuthors.AuthorId,
                        Name = BooksAuthors.Author.Name,
                        Character = BooksAuthors.Character,
                        Picture = BooksAuthors.Author.Picture,
                        Order = BooksAuthors.Order
                    });
                }
            }

            return result;
        }

        private List<BookshopDTO> MapBookshopsBooks(Book Book, BookDTO BookDTO)
        {
            var result = new List<BookshopDTO>();

            if (Book.BookshopsBooks != null)
            {
                foreach (var BookshopBooks in Book.BookshopsBooks)
                {
                    result.Add(new BookshopDTO()
                    {
                        Id = BookshopBooks.BookshopId,
                        Name = BookshopBooks.Bookshop.Name,
                        Latitude = BookshopBooks.Bookshop.Location.Y,
                        Longitude = BookshopBooks.Bookshop.Location.X
                    });
                }
            }

            return result;
        }

        private List<GenreDTO> MapBooksGenres(Book Book, BookDTO Bookdto)
        {
            var result = new List<GenreDTO>();

            if (Book.BooksGenres != null)
            {
                foreach (var genre in Book.BooksGenres)
                {
                    result.Add(new GenreDTO() { Id = genre.GenreId, Name = genre.Genre.Name });
                }
            }

            return result;
        }

        private List<BooksGenres> MapBooksGenres(BookCreationDTO BookCreationDTO, Book Book)
        {
            var result = new List<BooksGenres>();

            if (BookCreationDTO.GenresIds == null) { return result; }

            foreach (var id in BookCreationDTO.GenresIds)
            {
                result.Add(new BooksGenres() { GenreId = id });
            }

            return result;
        }

        private List<BookshopsBooks> MapBookshopsBooks(BookCreationDTO BookCreationDTO,
            Book Book)
        {
            var result = new List<BookshopsBooks>();

            if (BookCreationDTO.BookshopsIds == null) { return result; }

            foreach (var id in BookCreationDTO.BookshopsIds)
            {
                result.Add(new BookshopsBooks() { BookshopId = id });
            }

            return result;
        }

        private List<BooksAuthors> MapBooksAuthors(BookCreationDTO BookCreationDTO, Book Book)
        {
            var result = new List<BooksAuthors>();

            if (BookCreationDTO.Authors == null) { return result; }

            foreach (var Author in BookCreationDTO.Authors)
            {
                result.Add(new BooksAuthors() { AuthorId = Author.Id, Character = Author.Character });
            }

            return result;
        }
    }
}
