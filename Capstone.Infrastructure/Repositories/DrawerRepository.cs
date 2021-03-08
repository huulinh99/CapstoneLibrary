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

        public IEnumerable<DrawerDto> GetDrawerByListBook(IEnumerable<BookDto> books)
        {
            Dictionary<int,DrawerDto> list = new Dictionary<int,DrawerDto>();
            foreach (var book in books)
            {
                var entity = _entities.Where(x => x.Id == book.DrawerId)
                        .Select(x => new DrawerDto
                        {
                            Id = x.Id,
                            ShelfRow = x.ShelfRow,
                            ShelfColumn = x.ShelfColumn,
                            BookShelfName = x.BookShelf.Name,
                            BookShelfId = x.BookShelfId
                        })
                        .FirstOrDefault();
                if (!list.ContainsKey(entity.Id))
                {
                    list.Add(entity.Id, entity);
                }           
            }
            return list.Values;
        }       
    }
}
