using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IBookService
    {
        PagedList<BookDto> GetBooks(BookQueryFilter filters);
        Book GetBook(int id);
        void InsertBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(int?[] id);
    }
}
