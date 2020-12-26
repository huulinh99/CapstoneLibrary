using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        public BookGroupService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _mapper = mapper;
        }
        public async Task<bool> DeleteBookGroup(int id)
        {
            await _unitOfWork.BookGroupRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<BookGroup> GetBookGroup(int id)
        {
            return await _unitOfWork.BookGroupRepository.GetById(id);
        }

        public PagedList<BookGroup> GetBookGroups(BookGroupQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var bookGroups = _unitOfWork.BookGroupRepository.GetAll();
            if (filters.Name != null)
            {
                bookGroups = bookGroups.Where(x => x.Name.Contains(filters.Name));
            }

            if (filters.Fee != null)
            {
                bookGroups = bookGroups.Where(x => x.Fee == filters.Fee);
            }

            if (filters.PunishFee != null)
            {
                bookGroups = bookGroups.Where(x => x.PunishFee == filters.PunishFee);
            }
            var pagedBookGroups = PagedList<BookGroup>.Create(bookGroups, filters.PageNumber, filters.PageSize);
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
                    DrawerId = 4,
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
