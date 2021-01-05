using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IBookShelfRepository : IRepository<BookShelf>
    {
        Task<IEnumerable<BookShelfDto>> GetBookShelvesByLocation(int locationId);
        IEnumerable<BookShelfDto> GetBookShelvesAndLocationName();
    }
}
