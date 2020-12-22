using AutoMapper;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Infrastructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Book - BookDto
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();

            // Location - LocationDto
            CreateMap<Location, LocationDto>();
            CreateMap<LocationDto, Location>();

            // BookShelf - BookShelfDto
            CreateMap<BookShelf, BookShelfDto>();
            CreateMap<BookShelfDto, BookShelf>();

            // Drawer - DrawerDto
            CreateMap<Drawer, DrawerDto>();
            CreateMap<DrawerDto, Drawer>();
        }
    }
}
