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
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public BookService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public bool DeleteBook(int?[] id)
        {
            _unitOfWork.BookRepository.Delete(id);
            _unitOfWork.SaveChangesAsync();
            return true;
        }

        public BookDto GetBook(int id)
        {
            var book = _unitOfWork.BookRepository.GetBookByBookId(id);
            var returnDetail = _unitOfWork.ReturnDetailRepository.GetPatronByBookId(book.Id);
            var borrowDetail = _unitOfWork.BorrowDetailRepository.GetPatronByBookId(book.Id);
            if (borrowDetail != null && returnDetail == null)
            {
                //book.IsAvailable = false;
                var patron = _unitOfWork.PatronRepository.GetById(borrowDetail.PatronId);
                book.PatronId = patron.Id;
                book.PatronName = patron.Name;
                book.PatronImage = patron.Image;
                book.BorrowId = borrowDetail.BorrowId;

            }
            else if (borrowDetail != null && returnDetail != null)
            {
                //book.IsAvailable = true;
                var patron = _unitOfWork.PatronRepository.GetById(returnDetail.PatronId);
                book.PatronId = patron.Id;
                book.PatronName = patron.Name;
                book.PatronImage = patron.Image;
                book.BorrowId = borrowDetail.BorrowId;
            }
            return book;
        }

        public PagedList<BookDto> GetBooks(BookQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var books = _unitOfWork.BookRepository.GetAllBooks();

            if (filters.IsInDrawer != null)
            {
                books = books.Where(x => x.DrawerId == filters.DrawerId);
            }

            if (filters.IsAvailable != null)
            {
                books = books.Where(x => x.IsAvailable == filters.IsAvailable);
            }

            if (filters.IsDeleted != null)
            {
                books = books.Where(x => x.IsDeleted == filters.IsDeleted);
            }

            if (filters.Barcode != null)
            {
                books = books.Where(f => filters.Barcode.Contains(f.BarCode));
            }

            if (filters.IsInDrawer == false)
            {
                books = books.Where(x => x.DrawerId == null && x.IsDeleted == false);
            }

            if (filters.BookGroupId != null)
            {
                books = books.Where(x => x.BookGroupId == filters.BookGroupId);
            }

            if (filters.BookName != null)
            {
                books = books.Where(x => x.BookName.ToLower().Contains(filters.BookName.ToLower()));
            }

            if (filters.DrawerId != null)
            {
                books = books.Where(x => x.DrawerId == filters.DrawerId && x.IsDeleted == false);
            }
            var pagedBooks = PagedList<BookDto>.Create(books, filters.PageNumber, filters.PageSize);
            return pagedBooks;
        }

        public void InsertBook(Book book)
        {
            _unitOfWork.BookRepository.Add(book);
            var bookGroup = _unitOfWork.BookGroupRepository.GetById(book.BookGroupId);
            bookGroup.Quantity += 1;                     
            _unitOfWork.BookGroupRepository.Update(bookGroup);
            _unitOfWork.SaveChanges();
            char[] prefixs = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
                'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W','X', 'Y', 'Z' };
            int div = book.Id / 9999;
            char prefix = prefixs.ElementAt(div);
            string barcodeId = book.Id.ToString();
            string barcode = "";
            barcode += prefix;
            int sum = 0;
            for (int i = 0; i < barcodeId.Length; i++)
            {
                sum += int.Parse(barcodeId.ElementAt(i).ToString());
            }
            for (int i = 0; i < 4 - barcodeId.Length; i++)
            {
                barcode += "0";
            }
            barcode += barcodeId.ToString();
            if (sum < 10)
            {
                barcode += "0";
            }
            barcode += sum.ToString();
            book.Barcode = barcode;
            _unitOfWork.BookRepository.Update(book);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateBook(Book book)
        {
            //var entity = _unitOfWork.BookRepository.GetById(book.Id);
            //entity.DrawerId = book.DrawerId;         
            var tmp = _unitOfWork.BookRepository.GetById(book.Id);
            tmp.IsDeleted = book.IsDeleted;
            tmp.DrawerId = book.DrawerId;
            tmp.Note = book.Note;
            _unitOfWork.BookRepository.Update(tmp);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
