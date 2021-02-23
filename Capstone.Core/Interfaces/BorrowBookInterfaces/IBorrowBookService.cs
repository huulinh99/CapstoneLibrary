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
    public interface IBorrowBookService
    {
        PagedList<BorrowBookDto> GetBorrowBooks(BorrowBookQueryFilter filters);
        BorrowBook GetBorrowBook(int id);
        void InsertBorrowBook(BorrowBook borrowBook);
        bool UpdateBorrowBook(BorrowBook borrowBook);
        bool DeleteBorrowBook(int?[] id);
    }
}
