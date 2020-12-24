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
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
            //test commit
            CreateMap<BookGroup, BookGroupDto>();
            CreateMap<BookGroupDto, BookGroup>();

            CreateMap<Location, LocationDto>();
            CreateMap<LocationDto, Location>();

            CreateMap<BookShelf, BookShelfDto>();
            CreateMap<BookShelfDto, BookShelf>();

            CreateMap<Drawer, DrawerDto>();
            CreateMap<DrawerDto, Drawer>();
            //gggggg
        }
    }
}
