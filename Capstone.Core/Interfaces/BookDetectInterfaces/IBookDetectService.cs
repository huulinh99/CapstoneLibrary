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
        BookDetect GetBookDetect(int id);
        void InsertBookDetect(BookDetect bookDetect);
        bool UpdateBookDetect(BookDetect bookDetect);
        bool DeleteBookDetect(int?[] id);
    }
}
