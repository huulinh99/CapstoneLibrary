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
        public DrawerRepository(CapstoneContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<DrawerDto> GetAllDrawers(int bookShelfId, int rowStart, int rowEnd, int colStart, int colEnd)
        {
            List<DrawerDto> list = new List<DrawerDto>();
            for (int i = rowStart; i <= rowEnd; i++)
            {
                for (int j = colStart; j <= colEnd; j++)
                {
                    var entity = _entities.Where(x => x.ShelfRow == i && x.ShelfColumn == j && x.BookShelfId == bookShelfId)
                        .Select(x => new DrawerDto
                        {
                            Id = x.Id,
                            ShelfRow = i,
                            ShelfColumn = j,
                            BookShelfId = x.BookShelfId
                        })
                        .FirstOrDefault();
                    list.Add(entity);
                }
            }
            return list.AsEnumerable<DrawerDto>();
        }

        public void DeleteDrawerInBookShelf(int?[] bookShelfId)
        {
            var entities = _entities.Where(f => bookShelfId.Contains(f.BookShelfId)).ToList();
            entities.ForEach(a => a.IsDeleted = true);
            _context.SaveChangesAsync();
        }

        public int?[] GetDrawerIdInBookShelf(int?[] bookShelfId)
        {
            List<int?> termsList = new List<int?>();
            var entites = _entities.Where(f => bookShelfId.Contains(f.BookShelfId)).ToList();
            foreach (var entity in entites)
            {
                termsList.Add(entity.Id);
            }
            int?[] terms = termsList.ToArray();
            return terms;
        }

        public IEnumerable<DrawerDto> GetAllDrawers(IEnumerable<BookDrawer> bookDrawers)
        {
            List<DrawerDto> drawers = new List<DrawerDto>();
            foreach (var bookDrawer in bookDrawers)
            {
                var drawer = _entities.Where(x => x.BookDrawerId == bookDrawer.Id)
                    .Select(x => new DrawerDto
                    {
                        Id = x.Id,
                        BookShelfId = x.BookShelfId,
                        ShelfColumn = x.ShelfColumn,
                        ShelfRow = x.ShelfRow
                    }).FirstOrDefault();
                drawers.Add(drawer);
            }
            return drawers;
        }
    }
}
