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

            CreateMap<Location, LocationDto>();
            CreateMap<LocationDto, Location>();

            CreateMap<BookShelf, BookShelfDto>();
            CreateMap<BookShelfDto, BookShelf>();

            CreateMap<Drawer, DrawerDto>();
            CreateMap<DrawerDto, Drawer>();

            CreateMap<ErrorMessage, ErrorMessageDto>();
            CreateMap<ErrorMessageDto, ErrorMessage>();           

            CreateMap<BorrowBook, BorrowBookDto>();
            CreateMap<BorrowBookDto, BorrowBook>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<BorrowDetail, BorrowDetailDto>();
            CreateMap<BorrowDetailDto, BorrowDetail>();

            CreateMap<Campaign, CampaignDto>();
            CreateMap<CampaignDto, Campaign>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        
            CreateMap<ReturnBook, ReturnBookDto>();
            CreateMap<ReturnBookDto, ReturnBook>();

            CreateMap<ReturnDetail, ReturnDetailDto>();
            CreateMap<ReturnDetailDto, ReturnDetail>();

            CreateMap<Staff, StaffDto>();
            CreateMap<StaffDto, Staff>();

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<BookGroup, BookGroupDto>();
            CreateMap<BookGroupDto, BookGroup>();

            CreateMap<Image, ImageDto>();
            CreateMap<ImageDto, Image>();

            CreateMap<Feedback, FeedbackDto>();
            CreateMap<FeedbackDto, Feedback>();

            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationDto, Notification>();

            CreateMap<Staff, StaffDto>();
            CreateMap<StaffDto, Staff>();

            CreateMap<BookCategory, BookCategoryDto>();
            CreateMap<BookCategoryDto, BookCategory>();

            CreateMap<BookDrawer, BookDrawerDto>();
            CreateMap<BookDrawerDto, BookDrawer>();

            CreateMap<FavouriteCategory, FavouriteCategoryDto>();
            CreateMap<FavouriteCategoryDto, FavouriteCategory>();
        }
    }
}
