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
    public class DrawerRepository : BaseRepository<Drawer>, IDrawerRepository
    {
        private readonly CapstoneContext _context;
        public DrawerRepository(CapstoneContext context) : base(context) {
            _context = context;
        }
        public IEnumerable<DrawerDto> GetAllDrawers(int rowStart, int rowEnd, int colStart, int colEnd)
        {
            List<DrawerDto> list = new List<DrawerDto>();
            for (int i = rowStart; i <= rowEnd; i++)
            {
                for (int j = colStart; j <= colEnd; j++)
                {
                    var entity = _entities.Where(x=>x.ShelfRow == i && x.ShelfColumn == j)
                        .Select(x => new DrawerDto
                        {
                            ShelfRow = i,
                            ShelfColumn = j
                        })
                        .FirstOrDefault();
                    list.Add(entity);
                }
            }
            return list.AsEnumerable<DrawerDto>();
        }

        public async Task DeleteDrawerInBookShelf(int?[] bookShelfId)
        {
            var entities = _entities.Where(f => bookShelfId.Contains(f.BookShelfId)).ToList();
            entities.ForEach(a => a.IsDeleted = true);
            await _context.SaveChangesAsync();
        }
    }
}
