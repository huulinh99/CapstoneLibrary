﻿using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using Capstone.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class BookGroupRepository : BaseRepository<BookGroup>, IBookGroupRepository
    {

        public BookGroupRepository(CapstoneContext context) : base(context) {
        }
     
        public  IEnumerable<BookGroupDto> GetBookGroupsByName(string bookGroupName)
        {
            return  _entities.Where(x => x.Name.Contains(bookGroupName) && x.IsDeleted == false).Select(c => new BookGroupDto
            {
                Id = c.Id,
                Name = c.Name,
                Fee = c.Fee,
                PunishFee = c.PunishFee,
                Quantity = c.Quantity,
                Author = c.Author,
                PublishingPalace = c.PublishingPalace,
                PublishingCompany = c.PublishingCompany,
                PublishDate = c.PublishDate,
                Description = c.Description,
                PageNumber = c.PageNumber,
                Height = c.Height,
                Width = c.Width,
                Thick = c.Thick,
                PulishNumber = c.PulishNumber,
                Image = c.Image
            }).ToList();
        }

        public  IEnumerable<BookGroupDto> GetBookGroupsByBookCategory(IEnumerable<BookCategory> bookCategories)
        {
            List<BookGroupDto> bookGroups = new List<BookGroupDto>();
            foreach (var bookCategory in bookCategories)
            {
                var bookGroup = _entities.Where(x => x.Id == bookCategory.BookGroupId && x.IsDeleted == false)
                    .Select(c => new BookGroupDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Fee = c.Fee,
                    PunishFee = c.PunishFee,
                    Quantity = c.Quantity,
                    Author = c.Author,
                    PublishingPalace = c.PublishingPalace,
                    PublishingCompany = c.PublishingCompany,
                    PublishDate = c.PublishDate,
                    Description = c.Description,
                    PageNumber = c.PageNumber,
                    Height = c.Height,
                    Width = c.Width,
                    Thick = c.Thick,
                    PulishNumber = c.PulishNumber,
                    Image = c.Image
                    }).FirstOrDefault(); 
                bookGroups.Add(bookGroup);
            }
            return bookGroups;
        }       


        public async Task<BookGroupDto> GetBookGroupsWithImageById(int? bookGroupId, ICollection<CategoryDto> categories, ICollection<RatingDto> ratings)
        {
            
            var bookGroup = await _entities.Where(x => x.Id == bookGroupId)
                .Include(c => c.Image)
                .Include(s=>s.BookCategory)
                .ThenInclude(a=>a.Category)
                .Where(c => c.IsDeleted == false)
                .Select(c => new BookGroupDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Fee = c.Fee,
                    PunishFee = c.PunishFee,
                    Quantity = c.Quantity,                 
                    Author = c.Author,
                    PublishingPalace = c.PublishingPalace,
                    PublishingCompany = c.PublishingCompany,
                    PublishDate = c.PublishDate,                  
                    Description = c.Description,
                    PageNumber = c.PageNumber,                  
                    Height = c.Height,
                    Width = c.Width,
                    Thick = c.Thick,
                    PulishNumber = c.PulishNumber,
                    Image = c.Image,
                    Category = categories,
                    RatingAverage = Math.Round((double)c.Feedback.Sum(x => x.Rating) / (c.Feedback.Count), 2),
                    Rating = ratings
                }).FirstOrDefaultAsync();
            return bookGroup;
        }      
        

        public IEnumerable<BookGroupDto> GetAllBookGroupsWithCategory(IEnumerable<BookGroupDto> bookGroups, IEnumerable<BookCategory> bookCategories, IEnumerable<CategoryDto> categories)
        {

            Manage manage = new Manage();
            foreach (var rowBookCate in bookCategories)
            {
                foreach (var cate in categories)
                {
                    if(rowBookCate.CategoryId == cate.Id)
                    {
                        manage.add(rowBookCate.BookGroupId, cate);
                    }
                }
            }
         

            List<BookGroupDto> tmp = new List<BookGroupDto>();
            foreach (var bookGroup in bookGroups)
            {

                var cateTmp = manage.getItem(bookGroup.Id);

                var bookGroupDto = _entities.Where(x => x.Id == cateTmp.Id && x.IsDeleted == false)
                 .Select(c => new BookGroupDto
                 {
                     Id = c.Id,
                     Name = c.Name,
                     Fee = c.Fee,
                     PunishFee = c.PunishFee,
                     Quantity = c.Quantity,
                     Author = c.Author,
                     PublishingPalace = c.PublishingPalace,
                     PublishingCompany = c.PublishingCompany,
                     PublishDate = c.PublishDate,
                     Description = c.Description,
                     PageNumber = c.PageNumber,
                     Height = c.Height,
                     Width = c.Width,
                     Thick = c.Thick,
                     PulishNumber = c.PulishNumber,
                     Image = c.Image,
                     Category = cateTmp.listRecord,
                     RatingAverage = Math.Round((double)c.Feedback.Sum(x=>x.Rating)/(c.Feedback.Count),2)
                 }).FirstOrDefault();
                tmp.Add(bookGroupDto);
            }

            return tmp;
        }

        public IEnumerable<BookGroupDto> GetAllBookGroups()
        {

            var bookGroup = _entities
                .Include(c => c.Image)
                .Where(c => c.IsDeleted == false)
                .Select(c => new BookGroupDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Fee = c.Fee,
                    PunishFee = c.PunishFee,
                    Quantity = c.Quantity,
                    Author = c.Author,
                    PublishingPalace = c.PublishingPalace,
                    PublishingCompany = c.PublishingCompany,
                    PublishDate = c.PublishDate,
                    Description = c.Description,
                    PageNumber = c.PageNumber,
                    Height = c.Height,
                    Width = c.Width,
                    Thick = c.Thick,
                    PulishNumber = c.PulishNumber,
                    IsDeleted = c.IsDeleted,
                    Image = c.Image
                }).ToList();
            return bookGroup;
        }

        public IEnumerable<BookGroupDto> GetBookGroupsByAuthor(string author)
        {
            var bookGroup = _entities
                .Where(c=>c.Author.Contains(author.ToLower()))
                .Include(c => c.Image)
                .Include(s => s.BookCategory)
                .Select(c => new BookGroupDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Fee = c.Fee,
                    PunishFee = c.PunishFee,
                    Quantity = c.Quantity,
                    Author = c.Author,
                    PublishingPalace = c.PublishingPalace,
                    PublishingCompany = c.PublishingCompany,
                    PublishDate = c.PublishDate,
                    Description = c.Description,
                    PageNumber = c.PageNumber,
                    Height = c.Height,
                    Width = c.Width,
                    Thick = c.Thick,
                    PulishNumber = c.PulishNumber,
                    Image = c.Image
                }).ToList();
            return bookGroup;
        }
    }
}
