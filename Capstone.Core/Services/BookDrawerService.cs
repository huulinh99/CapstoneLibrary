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
        public bool DeleteBookDrawer(int?[] id)
        {
            _unitOfWork.BookDrawerRepository.GetBookDrawerByListBookId(id);
            _unitOfWork.SaveChangesAsync();
            _unitOfWork.BookRepository.GetBookByBookDrawerId(id);
            _unitOfWork.SaveChangesAsync();
            return true;
        }

        public PagedList<BookDrawer> GetBookDrawers(BookDrawerQueryFilter filters)
        {
            throw new NotImplementedException();
        }

        public BookDrawer GetBookDrawer(int id)
        {
            return _unitOfWork.BookDrawerRepository.GetById(id);
        }

        public void InsertBookDrawer(BookDrawer bookDrawer)
        {
            _unitOfWork.BookDrawerRepository.Add(bookDrawer);
            _unitOfWork.SaveChanges();
            var book = _unitOfWork.BookRepository.GetById(bookDrawer.BookId);
            book.BookDrawerId = bookDrawer.Id;
            _unitOfWork.BookRepository.Update(book);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateBookDrawer(BookDrawer bookDrawer)
        {
            var bookTemp =  _unitOfWork.BookDrawerRepository.GetBookDrawerByBookId(bookDrawer.BookId);
            bookTemp.IsDeleted = true;
            _unitOfWork.BookDrawerRepository.Update(bookTemp);
            _unitOfWork.SaveChangesAsync();
            _unitOfWork.BookDrawerRepository.Add(bookDrawer);
            _unitOfWork.SaveChangesAsync();
            return false;
        }
    }
}
