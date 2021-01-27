using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.Interfaces.BookDrawerInterfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class BookDrawerService : IBookDrawerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public BookDrawerService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteBookDrawer(int?[] id)
        {
            await _unitOfWork.BookDrawerRepository.GetBookDrawerByListBookId(id);
            await _unitOfWork.SaveChangesAsync();
            _unitOfWork.BookRepository.GetBookByBookDrawerId(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public PagedList<BookDrawer> GetBookDrawers(BookDrawerQueryFilter filters)
        {
            throw new NotImplementedException();
        }

        public async Task<BookDrawer> GetBookDrawer(int id)
        {
            return await _unitOfWork.BookDrawerRepository.GetById(id);
        }

        public async Task InsertBookDrawer(BookDrawer bookDrawer)
        {
            await _unitOfWork.BookDrawerRepository.Add(bookDrawer);
            await _unitOfWork.SaveChangesAsync();
            var book = _unitOfWork.BookRepository.GetById(bookDrawer.BookId).Result;
            book.BookDrawerId = bookDrawer.Id;
             _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateBookDrawer(BookDrawer bookDrawer)
        {
            var bookTemp =  _unitOfWork.BookDrawerRepository.GetBookDrawerByBookId(bookDrawer.BookId);
            bookTemp.IsDeleted = true;
            _unitOfWork.BookDrawerRepository.Update(bookTemp);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.BookDrawerRepository.Add(bookDrawer);
            await _unitOfWork.SaveChangesAsync();
            return false;
        }
    }
}
