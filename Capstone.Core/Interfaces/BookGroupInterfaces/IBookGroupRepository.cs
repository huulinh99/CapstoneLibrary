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
        IEnumerable<BookGroupDto> GetBookGroupsByName(string bookGroupName);
        IEnumerable<BookGroupDto> GetBookGroupsByBookCategory(IEnumerable<BookCategory> bookCategory);
        IEnumerable<BookGroupDto> GetBookGroupsByAuthor(string author);
        Task<BookGroupDto> GetBookGroupsWithImageById(int? bookGroupId, ICollection<CategoryDto> categories, ICollection<RatingDto> ratings);
        IEnumerable<BookGroupDto> GetAllBookGroupsWithCategory(IEnumerable<BookGroupDto> bookGroups, IEnumerable<BookCategory> bookCategories, IEnumerable<CategoryDto> categories);
        IEnumerable<BookGroupDto> GetAllBookGroups();
    }
}


