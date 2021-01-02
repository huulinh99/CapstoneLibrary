using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IBookGroupRepository : IRepository<BookGroup>
    {
        Task<IEnumerable<BookGroupDto>> GetBookGroupsByName(string bookGroupName, ICollection<CategoryDto> categories);
        IEnumerable<BookGroupDto> GetBookGroupsByBookCategory(IEnumerable<BookCategory> bookCategory, ICollection<CategoryDto> categories);
        Task<BookGroupDto> GetBookGroupsWithImageById(int? bookGroupId, ICollection<CategoryDto> categories);
        IEnumerable<BookGroupDto> GetAllBookGroups(ICollection<CategoryDto> categories);
    }
}


