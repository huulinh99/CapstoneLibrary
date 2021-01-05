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
    public class BookShelfRepository : BaseRepository<BookShelf>, IBookShelfRepository
    {
        public BookShelfRepository(CapstoneContext context) : base(context) { }
        public async Task<IEnumerable<BookShelfDto>> GetBookShelvesByLocation(int locationId)
        {
            return await _entities.Where(x => x.LocationId == locationId).Select(x => new BookShelfDto
            {
                Id = x.Id,
                Name = x.Name,
                LocationColor = x.Location.Color,
                LocationName = x.Location.Name,
                Col = x.Col,
                Row = x.Row,
                LocationId = x.Location.Id
            }).ToListAsync();
        }

        public IEnumerable<BookShelfDto> GetBookShelvesAndLocationName()
        {
            return  _entities.Include(x => x.Location).Where(x=>x.IsDeleted == false).Select(x => new BookShelfDto
            {
                Id = x.Id,
                Name = x.Name,
                LocationColor = x.Location.Color,
                LocationName = x.Location.Name,
                Col = x.Col,
                Row = x.Row,
                LocationId = x.Location.Id
            }).ToList();
        }
    }
}
