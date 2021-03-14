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
        public IEnumerable<DrawerDto> GetAllDrawers(int? bookShelfId, int rowStart, int rowEnd, int colStart, int colEnd)
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
                            BookShelfId = x.BookShelfId,
                            DrawerBarcode = x.DrawerBarcode
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
            List<DrawerDto> list = new List<DrawerDto>();
            foreach (var book in books)
            {
                var entity = _entities.Where(x => x.Id == book.DrawerId)
                        .Select(x => new DrawerDto
                        {
                            Id = x.Id,
                            ShelfRow = x.ShelfRow,
                            ShelfColumn = x.ShelfColumn,
                            BookShelfName = x.BookShelf.Name,
                            BookGroupId = book.BookGroupId,
                            BookId = book.Id,
                            BookShelfId = x.BookShelfId,
                            DrawerBarcode = x.DrawerBarcode
                        })
                        .FirstOrDefault();
                list.Add(entity);
            }
            return list;
        }

        public IEnumerable<DrawerDto> GetDrawerByBookShelfId(int? bookShelfId)
        {
            return _entities.Where(x => x.BookShelfId == bookShelfId && x.IsDeleted == false)
                .Select(c => new DrawerDto
                {
                    Id = c.Id,
                    BookShelfId = c.BookShelfId,
                    BookShelfName = c.BookShelf.Name,
                    ShelfColumn = c.ShelfColumn,
                    ShelfRow = c.ShelfRow,
                    DrawerBarcode = c.DrawerBarcode
                }).ToList();
        }
    }
}
