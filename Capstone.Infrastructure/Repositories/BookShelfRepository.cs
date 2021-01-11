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
        private readonly CapstoneContext _context;
        public BookShelfRepository(CapstoneContext context) : base(context) {
            _context = context;
        }
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

        public async Task DeleteBookShelfInLocation(int?[] locationId)
        {
            var entities = _entities.Where(f => locationId.Contains(f.LocationId)).ToList();
            entities.ForEach(a => a.IsDeleted = true);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<BookShelfDto> GetBookShelvesByDrawer(IEnumerable<DrawerDto> drawers)
        {
            Dictionary<int,BookShelfDto> list = new Dictionary<int,BookShelfDto>();
            foreach (var drawer in drawers)
            {
                var bookShelf = _entities.Where(x => x.Id == drawer.BookShelfId).Select(x => new BookShelfDto
                {
                    Id = x.Id,
                    Col = x.Col,
                    LocationColor = x.Location.Color,
                    LocationId = x.LocationId,
                    LocationName = x.Location.Name,
                    Name = x.Name,
                    Row = x.Row
                }).FirstOrDefault();
                if (!list.ContainsKey(bookShelf.Id))
                {
                    list.Add(bookShelf.Id,bookShelf);
                }
            }

            return list.Values.ToList<BookShelfDto>();
        }

        public int?[] GetBookShelfIdInLocation(int?[] locationId)
        {
            List<int?> termsList = new List<int?>();
            var entites =  _entities.Where(f => locationId.Contains(f.LocationId)).ToList();
            foreach (var entity in entites)
            {
                termsList.Add(entity.Id);
            }
            int?[] terms = termsList.ToArray();
            return terms;
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
