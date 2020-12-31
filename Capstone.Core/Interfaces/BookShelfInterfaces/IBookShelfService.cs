using Capstone.Core.CustomEntities;
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
        PagedList<BookShelf> GetBookShelves(BookShelfQueryFilter filters);
        Task<BookShelf> GetBookShelf(int id);
        Task InsertBookShelf(BookShelf bookShelf);
        Task<bool> UpdateBookShelf(BookShelf bookShelf);
        Task<bool> DeleteBookShelf(int id);
    }
}
