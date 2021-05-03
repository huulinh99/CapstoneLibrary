using Capstone.Core.DTOs;
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

        public BookGroupRepository(CapstoneContext context) : base(context)
        {
        }

        public IEnumerable<BookGroupDto> GetBookGroupsByName(string bookGroupName)
        {
            return _entities.Include(c => c.Image).Where(x => x.Name.ToLower().Contains(bookGroupName.ToLower()) && x.IsDeleted == false).Select(c => new BookGroupDto
            {
                Id = c.Id,
                Name = c.Name,
                Fee = c.Fee,
                PunishFee = c.PunishFee,
                Quantity = c.Quantity,
                Author = c.Author,
                PublishPlace = c.PublishPlace,
                PublishCompany = c.PublishCompany,
                PublishDate = c.PublishDate,
                Description = c.Description,
                PageNumber = c.PageNumber,
                StaffId = c.StaffId,
                StaffName = c.Staff.Name,
                Height = c.Height,
                Width = c.Width,
                CreatedDate = c.CreatedDate,
                Thick = c.Thick,
                Edition = c.Edition,
                Image = c.Image.Where(x => x.IsDeleted == false).ToList()
            }).ToList();
        }

        public IEnumerable<BookGroupDto> GetBookGroupsByBookCategory(IEnumerable<BookCategory> bookCategories)
        {
            List<BookGroupDto> bookGroups = new List<BookGroupDto>();
            foreach (var bookCategory in bookCategories)
            {
                var bookGroup = _entities.Include(c => c.Image).Where(x => x.Id == bookCategory.BookGroupId && x.IsDeleted == false)
                    .Select(c => new BookGroupDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Fee = c.Fee,
                        PunishFee = c.PunishFee,
                        Quantity = c.Quantity,
                        Author = c.Author,
                        PublishPlace = c.PublishPlace,
                        PublishCompany = c.PublishCompany,
                        PublishDate = c.PublishDate,
                        Description = c.Description,
                        PageNumber = c.Edition,
                        Height = c.Height,
                        Width = c.Width,
                        CreatedDate = c.CreatedDate,
                        StaffId = c.StaffId,
                        StaffName = c.Staff.Name,
                        Thick = c.Thick,
                        Edition = c.Edition,
                        Image = c.Image.Where(x => x.IsDeleted == false).ToList()
                    }).FirstOrDefault();
                bookGroups.Add(bookGroup);
            }
            return bookGroups;
        }
       


        public BookGroupDto GetBookGroupsWithImageById(int? bookGroupId, ICollection<CategoryDto> categories, ICollection<RatingDto> ratings)
        {
            var bookGroup = _entities.Where(x => x.Id == bookGroupId)
                .Where(c => c.IsDeleted == false)
                .Select(c => new BookGroupDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Fee = c.Fee,
                    PunishFee = c.PunishFee,
                    Quantity = c.Quantity,
                    Author = c.Author,
                    PublishPlace = c.PublishPlace,
                    PublishCompany = c.PublishCompany,
                    PublishDate = c.PublishDate,
                    Description = c.Description,
                    StaffId = c.StaffId,
                    StaffName = c.Staff.Name,
                    PageNumber = c.PageNumber,
                    Price = c.Price,
                    Height = c.Height,
                    Width = c.Width,
                    CreatedDate = c.CreatedDate,
                    Thick = c.Thick,
                    Edition = c.Edition,
                    Image = c.Image.Where(x => x.IsDeleted == false).ToList(),
                    Category = categories,
                    RatingAverage = Math.Round((double)c.Feedback.Where(x => x.IsDeleted == false).Sum(x => x.Rate) / (c.Feedback.Where(x=>x.IsDeleted == false).ToList().Count), 2),
                    Rating = ratings
                }).FirstOrDefault();
            return bookGroup;
        }


        public IEnumerable<BookGroupDto> GetAllBookGroupsWithCategory(IEnumerable<BookGroupDto> bookGroups, IEnumerable<BookCategory> bookCategories, IEnumerable<CategoryDto> categories)
        {

            Manage manage = new Manage();
            foreach (var rowBookCate in bookCategories)
            {
                foreach (var cate in categories)
                {
                    if (rowBookCate.CategoryId == cate.Id)
                    {
                        manage.add(rowBookCate.BookGroupId, cate);
                    }
                }
            }


            List<BookGroupDto> tmp = new List<BookGroupDto>();
            foreach (var bookGroup in bookGroups)
            {

                var cateTmp = manage.getItem(bookGroup.Id);
                if (cateTmp == null)
                {
                    var bookGroupDto = _entities.Include(c => c.Image).Where(x => x.IsDeleted == false && x.Id == bookGroup.Id)
                 .Select(c => new BookGroupDto
                 {
                     Id = c.Id,
                     Name = c.Name,
                     Fee = c.Fee,
                     PunishFee = c.PunishFee,
                     Quantity = c.Quantity,
                     Author = c.Author,
                     PublishPlace = c.PublishPlace,
                     PublishCompany = c.PublishCompany,
                     PublishDate = c.PublishDate,
                     Description = c.Description,
                     PageNumber = c.PageNumber,
                     Height = c.Height,
                     StaffId = c.StaffId,
                     StaffName = c.Staff.Name,
                     Width = c.Width,
                     CreatedDate = c.CreatedDate,
                     Thick = c.Thick,
                     Edition = c.Edition,
                     Image = c.Image.Where(x => x.IsDeleted == false).ToList(),
                     RatingAverage = Math.Round((double)c.Feedback.Where(x=>x.IsDeleted==false).Sum(x => x.Rate) / (c.Feedback.Where(x => x.IsDeleted == false).ToList().Count), 2)
                 }).OrderByDescending(x=>x.Id).FirstOrDefault();
                    tmp.Add(bookGroupDto);
                }
                else
                {
                    var bookGroupDto = _entities.Include(c => c.Image).Where(x => x.Id == cateTmp.Id && x.IsDeleted == false)
                    .Select(c => new BookGroupDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Fee = c.Fee,
                        PunishFee = c.PunishFee,
                        Quantity = c.Quantity,
                        Author = c.Author,
                        PublishPlace = c.PublishPlace,
                        PublishCompany = c.PublishCompany,
                        PublishDate = c.PublishDate,
                        Description = c.Description,
                        PageNumber = c.PageNumber,
                        Height = c.Height,
                        Width = c.Width,
                        StaffId = c.StaffId,
                        StaffName = c.Staff.Name,
                        Thick = c.Thick,
                        CreatedDate = c.CreatedDate,
                        Edition = c.Edition,
                        Image = c.Image.Where(x => x.IsDeleted == false).ToList(),
                        Category = cateTmp.listRecord,
                        RatingAverage = Math.Round((double)c.Feedback.Where(x => x.IsDeleted == false).Sum(x => x.Rate) / (c.Feedback.Where(x => x.IsDeleted == false).ToList().Count), 2)
                    }).OrderByDescending(x => x.Id).FirstOrDefault();

                    tmp.Add(bookGroupDto);
                }


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
                    PublishPlace = c.PublishPlace,
                    PublishCompany = c.PublishCompany,
                    PublishDate = c.PublishDate,
                    Description = c.Description,
                    PageNumber = c.PageNumber,
                    Height = c.Height,
                    StaffId = c.StaffId,
                    StaffName = c.Staff.Name,
                    Width = c.Width,
                    CreatedDate = c.CreatedDate,
                    Thick = c.Thick,
                    Edition = c.Edition,
                    IsDeleted = c.IsDeleted,
                    Image = c.Image.Where(x => x.IsDeleted == false).ToList()
                }).ToList();
            return bookGroup;
        }

        public IEnumerable<BookGroupDto> GetBookGroupsByAuthor(string author)
        {
            var bookGroup = _entities
                .Where(c => c.Author.Contains(author.ToLower()))
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
                    PublishPlace = c.PublishPlace,
                    PublishCompany = c.PublishCompany,
                    PublishDate = c.PublishDate,
                    Description = c.Description,
                    PageNumber = c.PageNumber,
                    Height = c.Height,
                    Width = c.Width,
                    CreatedDate = c.CreatedDate,
                    StaffId = c.StaffId,
                    StaffName = c.Staff.Name,
                    Thick = c.Thick,
                    Edition = c.Edition,
                    Image = c.Image.Where(x => x.IsDeleted == false).ToList()
                }).ToList();
            return bookGroup;
        }
    }
}
