using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
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
     
        public  IEnumerable<BookGroupDto> GetBookGroupsByName(string bookGroupName, ICollection<CategoryDto> categories)
        {
            return  _entities.Where(x => x.Name.Contains(bookGroupName)).Select(c => new BookGroupDto
            {
                Id = c.Id,
                Name = c.Name,
                Fee = c.Fee,
                PunishFee = c.PunishFee,
                Quantity = c.Quantity,
                Author = c.Author,
                PublishingPalace = c.PublishingPlace,
                PublishingCompany = c.PublishingCompany,
                PublishDate = c.PublishDate,
                Description = c.Description,
                PageNumber = c.PageNumber,
                Height = c.Height,
                Width = c.Width,
                Thick = c.Thick,
                PublishNumber = c.PulishNumber,
                Image = c.Image,
                Category = categories
            }).ToList();
        }

        public  IEnumerable<BookGroupDto> GetBookGroupsByBookCategory(IEnumerable<BookCategory> bookCategories, ICollection<CategoryDto> categories)
        {
            List<BookGroupDto> bookGroups = new List<BookGroupDto>();
            foreach (var bookCategory in bookCategories)
            {
                var bookGroup = _entities.Where(x => x.Id == bookCategory.BookGroupId)
                    .Select(c => new BookGroupDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Fee = c.Fee,
                    PunishFee = c.PunishFee,
                    Quantity = c.Quantity,
                    Author = c.Author,
                    PublishingPalace = c.PublishingPlace,
                    PublishingCompany = c.PublishingCompany,
                    PublishDate = c.PublishDate,
                    Description = c.Description,
                    PageNumber = c.PageNumber,
                    Height = c.Height,
                    Width = c.Width,
                    Thick = c.Thick,
                    PublishNumber = c.PulishNumber,
                    Image = c.Image,
                    Category = categories
                    }).FirstOrDefault(); 
                bookGroups.Add(bookGroup);
            }
            return bookGroups;
        }

        public async Task<BookGroupDto> GetBookGroupsWithImageById(int? bookGroupId, ICollection<CategoryDto> categories)
        {
            
            var bookGroup = await _entities.Where(x => x.Id == bookGroupId)
                .Include(c => c.Image)
                .Include(s=>s.BookCategory)
                .Select(c => new BookGroupDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Fee = c.Fee,
                    PunishFee = c.PunishFee,
                    Quantity = c.Quantity,                 
                    Author = c.Author,
                    PublishingPalace = c.PublishingPlace,
                    PublishingCompany = c.PublishingCompany,
                    PublishDate = c.PublishDate,                  
                    Description = c.Description,
                    PageNumber = c.PageNumber,                  
                    Height = c.Height,
                    Width = c.Width,
                    Thick = c.Thick,
                    PublishNumber = c.PulishNumber,
                    Image = c.Image,
                    Category = categories
                }).FirstOrDefaultAsync();
            return bookGroup;
        }

        public  IEnumerable<BookGroupDto> GetAllBookGroups(ICollection<CategoryDto> categories)
        {

            var bookGroup =  _entities
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
                    PublishingPalace = c.PublishingPlace,
                    PublishingCompany = c.PublishingCompany,
                    PublishDate = c.PublishDate,
                    Description = c.Description,
                    PageNumber = c.PageNumber,
                    Height = c.Height,
                    Width = c.Width,
                    Thick = c.Thick,
                    PublishNumber = c.PulishNumber,
                    Image = c.Image,
                    Category = categories
                }).ToList();
            return bookGroup;
        }

        public IEnumerable<BookGroupDto> GetBookGroupsByAuthor(string author, ICollection<CategoryDto> categories)
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
                    PublishingPalace = c.PublishingPlace,
                    PublishingCompany = c.PublishingCompany,
                    PublishDate = c.PublishDate,
                    Description = c.Description,
                    PageNumber = c.PageNumber,
                    Height = c.Height,
                    Width = c.Width,
                    Thick = c.Thick,
                    PublishNumber = c.PulishNumber,
                    Image = c.Image,
                    Category = categories
                }).ToList();
            return bookGroup;
        }
    }
}
