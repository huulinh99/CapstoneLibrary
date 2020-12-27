using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IBookDetectService
    {
        PagedList<BookDetect> GetBookDetect(BookDetectQueryFilter filters);
        Task<BookDetect> GetBookDetect(int id);
        Task InsertBookDetect(BookDetect bookDetect);
        Task<bool> UpdateBookDetect(BookDetect bookDetect);
        Task<bool> DeleteBookDetect(int id);
    }
}
