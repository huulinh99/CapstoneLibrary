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
    public interface IReturnBookService
    {
        PagedList<ReturnBookDto> GetReturnBooks(ReturnBookQueryFilter filters);
        ReturnBook GetReturnBook(int id);
        void InsertReturnBook(ReturnBook returnBook);
        bool UpdateReturnBook(ReturnBook returnBook);

        bool DeleteReturnBook(int?[] id);
    }
}
