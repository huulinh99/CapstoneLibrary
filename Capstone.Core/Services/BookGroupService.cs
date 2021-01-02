using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<bool> DeleteBookGroup(int id)
        {
            await _unitOfWork.BookGroupRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<BookGroupDto> GetBookGroup(int id)
        {
            var bookCategories = await _unitOfWork.BookCategoryRepository.GetBookCategoriesByBookGroup(id);
            var categories =  _unitOfWork.CategoryRepository.GetCategoryNameByBookCategory(bookCategories);
            return await _unitOfWork.BookGroupRepository.GetBookGroupsWithImageById(id, categories);
        }

        public PagedList<BookGroupDto> GetBookGroups(BookGroupQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var categories = _unitOfWork.CategoryRepository.GetAllCategories();
            var bookGroups = _unitOfWork.BookGroupRepository.GetAllBookGroups(categories);
            if (filters.Name != null)
            {
                bookGroups = bookGroups.Where(x => x.Name.Contains(filters.Name));
            }

            if (filters.Author != null)
            {
                bookGroups = bookGroups.Where(x => x.Author.Contains(filters.Author));
            }

            if (filters.CategoryId != null)
            {
                var categoryByCategory = _unitOfWork.BookCategoryRepository.GetBookCategoriesByCategory(filters.CategoryId).Result;
                bookGroups = _unitOfWork.BookGroupRepository.GetBookGroupsByBookCategory(categoryByCategory, categories);
            }

            if (filters.Fee != null)
            {
                bookGroups = bookGroups.Where(x => x.Fee == filters.Fee);
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
            for (int i= 0; i < bookGroup.Quantity; i++) 
            {
                var bookModel = new Book()
                {
                    BookGroupId = bookGroup.Id,
                    BarCode = null
                };
                 _unitOfWork.BookRepository.Add(bookModel);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateBookGroup(BookGroup bookGroup)
        {
            _unitOfWork.BookGroupRepository.Update(bookGroup);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
