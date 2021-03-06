﻿using Capstone.Core.CustomEntities;
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

        public Book GetBook(int id)
        {
            return _unitOfWork.BookRepository.GetById(id);
        }

        public PagedList<BookDto> GetBooks(BookQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var books = _unitOfWork.BookRepository.GetAllBooks();

            
         
            if (filters.IsInDrawer == true)
            {
                books = _unitOfWork.BookRepository.GetAllBooksInDrawer();
            }

            if(filters.Barcode != null)
            {
                books = _unitOfWork.BookRepository.GetBookByListId(filters.Barcode);
                foreach (var book in books)
                {
                    var returnDetail = _unitOfWork.ReturnDetailRepository.GetCustomerByBookId(book.Id);
                    if (returnDetail == null)
                    {
                        book.IsAvailable = true;
                    }
                    else
                    {
                        var customer = _unitOfWork.CustomerRepository.GetById(returnDetail.CustomerId);
                        book.CustomerId = customer.Id;
                        book.CustomerName = customer.Name;
                        book.CustomerImage = customer.Image;
                    }                   
                }
            }

            if (filters.IsInDrawer == false)
            {
                books = _unitOfWork.BookRepository.GetAllBooksNotInDrawer();
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
                books = _unitOfWork.BookRepository.GetBookByDrawer(filters.DrawerId);
            }
            var pagedBooks = PagedList<BookDto>.Create(books, filters.PageNumber, filters.PageSize);
            return pagedBooks;
        }

        public void InsertBook(Book book)
        {     
            _unitOfWork.BookRepository.Add(book);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateBook(Book book)
        {
            var entity = _unitOfWork.BookRepository.GetById(book.Id);
            entity.DrawerId = book.DrawerId;
            _unitOfWork.BookRepository.Update(entity);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
