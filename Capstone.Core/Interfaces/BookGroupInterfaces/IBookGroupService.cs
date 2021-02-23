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
    public interface IBookGroupService
    {
        PagedList<BookGroupDto> GetBookGroups(BookGroupQueryFilter filters);
        BookGroupDto GetBookGroup(int id);
        void InsertBookGroup(BookGroup bookGroup);
        bool UpdateBookGroup(BookGroup bookGroup);
        bool DeleteBookGroup(int?[] id);
    }
}
