using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class BookGroupRepository : BaseRepository<BookGroup>, IBookGroupRepository
    {
        public BookGroupRepository(CapstoneContext context) : base(context) { }
     
        public async Task<IEnumerable<BookGroup>> GetBookGroupsByName(string bookGroupName)
        {
            return await _entities.Where(x => x.Name.Contains(bookGroupName)).ToListAsync();
        }

        public  IEnumerable<BookGroup> GetBookGroupsByBookCategory(IEnumerable<BookCategory> bookCategories)
        {
            List<BookGroup> bookGroups = new List<BookGroup>();
            foreach (var bookCategory in bookCategories)
            {
                var bookGroup = _entities.Where(x => x.Id == bookCategory.BookGroupId).FirstOrDefault();
                bookGroups.Add(bookGroup);
            }
            return bookGroups;
        }

        public async Task<BookGroup> GetBookGroupsWithImageById(int bookGroupId)
        {
            var bookGroup = await _entities.Where(x => x.Id == bookGroupId).Include(c => c.Image)
                .SingleOrDefaultAsync();
            return bookGroup;
        }
    }
}
