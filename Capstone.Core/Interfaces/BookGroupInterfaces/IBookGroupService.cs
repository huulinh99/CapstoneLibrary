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
        Task<BookGroupDto> GetBookGroup(int id);
        Task InsertBookGroup(BookGroup bookGroup);
        Task<bool> UpdateBookGroup(BookGroup bookGroup);
        Task<bool> DeleteBookGroup(int?[] id);
    }
}
