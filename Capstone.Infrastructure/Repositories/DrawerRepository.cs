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
                    var entity = _entities.Where(x => x.Row == i && x.Col == j && x.BookShelfId == bookShelfId)
                        .Select(x => new DrawerDto
                        {
                            Id = x.Id,
                            Row = i,
                            Col = j,
                            BookShelfId = x.BookShelfId,
                            Name = x.Name,
                            Barcode = x.Barcode
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
                            Row = x.Row,
                            Col = x.Col,
                            BookShelfName = x.BookShelf.Name,
                            BookGroupId = book.BookGroupId,
                            BookId = book.Id,
                            BookShelfId = x.BookShelfId,
                            Barcode = x.Barcode
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
                    Col = c.Col,
                    Row = c.Row,
                    Barcode = c.Barcode
                }).ToList();
        }
    }
}
