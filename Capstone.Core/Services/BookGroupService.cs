using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class BookGroupService : IBookGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public BookGroupService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteBookGroup(int?[] id)
        {
            await _unitOfWork.BookGroupRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<BookGroupDto> GetBookGroup(int id)
        {
            var bookCategories =  _unitOfWork.BookCategoryRepository.GetBookCategoriesByBookGroup(id);
            var categories =  _unitOfWork.CategoryRepository.GetCategoryNameByBookCategory(bookCategories);
            var feedback = _unitOfWork.FeedbackRepository.GetRatingForBookGroup(id);
            return await _unitOfWork.BookGroupRepository.GetBookGroupsWithImageById(id, categories, feedback);
        }

        public PagedList<BookGroupDto> GetBookGroups(BookGroupQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var bookGroupDtos = _unitOfWork.BookGroupRepository.GetAllBookGroups();
           
            var categories = _unitOfWork.CategoryRepository.GetAllCategories();

            var bookCategories = _unitOfWork.BookCategoryRepository.GetAllBookCategoriesByBookGroup();

            var bookGroups = _unitOfWork.BookGroupRepository.GetAllBookGroupsWithCategory(bookGroupDtos, bookCategories, categories);

            if (filters.Name != null)
            {
                bookGroups = bookGroups.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }
            if (filters.CustomerId != null)
            {
                var favourite = _unitOfWork.FavouriteCategoryRepository.GetFavouriteCategoryForSuggest(filters.CustomerId);
                var categoryByCategory = _unitOfWork.BookCategoryRepository.GetBookCategoriesByCategory(favourite.CategoryId);
                bookGroups = _unitOfWork.BookGroupRepository.GetBookGroupsByBookCategory(categoryByCategory);
            }
            if (filters.Author != null)
            {
                bookGroups = bookGroups.Where(x => x.Author.ToLower().Contains(filters.Author.ToLower()));
            }

            if (filters.Fee != null)
            {
                bookGroups = bookGroups.Where(x => x.Fee == filters.Fee);
            }

            if (filters.CategoryId != null)
            {
                var categoryByCategory = _unitOfWork.BookCategoryRepository.GetBookCategoriesByCategory(filters.CategoryId);
                bookGroups = _unitOfWork.BookGroupRepository.GetBookGroupsByBookCategory(categoryByCategory);
            }

            if (filters.PunishFee != null)
            {
                bookGroups = bookGroups.Where(x => x.PunishFee == filters.PunishFee);
            }

            var pagedBookGroups = PagedList<BookGroupDto>.Create(bookGroups, filters.PageNumber, filters.PageSize);
            return pagedBookGroups;
        }


        public async Task InsertBookGroup(BookGroup bookGroup)
        {
            await _unitOfWork.BookGroupRepository.Add(bookGroup);
            await _unitOfWork.SaveChangesAsync();
            foreach (var bookCategory in bookGroup.BookCategory)
            {
                bookCategory.IsDeleted = false;
            }

            foreach (var image in bookGroup.Image)
            {
                image.IsDeleted = false;
            }
            for (int i= 0; i < bookGroup.Quantity; i++) 
            {
                var bookModel = new Book()
                {
                    BookGroupId = bookGroup.Id,
                    BarCode = null
                };
                 await _unitOfWork.BookRepository.Add(bookModel);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateBookGroup(BookGroup bookGroup)
        {
            var images = _unitOfWork.ImageRepository.GetImageByBookGroupId(bookGroup.Id);
            var bookGroupDto = _unitOfWork.BookGroupRepository.GetById(bookGroup.Id).Result;
            var imageId = new List<int>();          
            if (bookGroup.Image.Count == 0)
            {
                foreach (var image in images)
                {
                    image.IsDeleted = true;
                    _unitOfWork.ImageRepository.Update(image);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            else
            {              
                foreach (var image in bookGroup.Image)
                {
                    if (image.Id == 0)
                    {
                        image.IsDeleted = false;
                        image.BookGroupId = bookGroup.Id;
                        await _unitOfWork.ImageRepository.Add(image);
                        await _unitOfWork.SaveChangesAsync();
                    }
                    else
                    {
                        imageId.Add(image.Id);
                    }                   
                }
                var entities = images.Where(f => !imageId.Contains(f.Id)).ToList();
                entities.ForEach(a => a.IsDeleted = true);            
            }
            bookGroupDto.Name = bookGroup.Name;
            bookGroupDto.Fee = bookGroup.Fee;
            bookGroupDto.PunishFee = bookGroup.PunishFee;
            bookGroupDto.Quantity = bookGroup.Quantity;
            bookGroupDto.Author = bookGroup.Author;
            bookGroupDto.PublishingPalace = bookGroup.PublishingPalace;
            bookGroupDto.PublishingCompany = bookGroup.PublishingCompany;
            bookGroupDto.PublishDate = bookGroup.PublishDate;
            bookGroupDto.Description = bookGroup.Description;
            bookGroupDto.PageNumber = bookGroup.PageNumber;
            bookGroupDto.Height = bookGroup.Height;
            bookGroupDto.Width = bookGroup.Width;
            bookGroupDto.Thick = bookGroup.Thick;
            bookGroupDto.PublishNumber = bookGroup.PublishNumber;
            bookGroupDto.IsDeleted = false;
            _unitOfWork.BookGroupRepository.Update(bookGroupDto);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
