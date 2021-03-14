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
    public interface IBookShelfService
    {
        PagedList<BookShelfDto> GetBookShelves(BookShelfQueryFilter filters);
        BookShelf GetBookShelf(int id);
        void InsertBookShelf(BookShelf bookShelf);
        bool UpdateBookShelf(BookShelf bookShelf);
        bool DeleteBookShelf(int?[] id);
    }
}
