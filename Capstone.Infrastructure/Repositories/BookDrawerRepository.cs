using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces.BookDrawerInterfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class BookDrawerRepository : BaseRepository<BookDrawer>, IBookDrawerRepository
    {
        private readonly CapstoneContext _context;
        public BookDrawerRepository(CapstoneContext context) : base(context) {
            _context = context;
        }

        public IEnumerable<BookDrawer> GetBookDrawerByDrawerId(int? drawerId)
        {
            return _entities.Where(x => x.DrawerId == drawerId && x.IsDeleted == false).ToList();
        }

        public int?[] GetBookDrawerIdInDrawer(int?[] drawerId)
        {
            List<int?> termsList = new List<int?>();
            var entites = _entities.Where(f => drawerId.Contains(f.DrawerId)).ToList();
            foreach (var entity in entites)
            {
                termsList.Add(entity.Id);
            }
            int?[] terms = termsList.ToArray();
            return terms;
        }

        public BookDrawer GetBookDrawerByBookId(int? bookId)
        {
            return _entities.Where(x => x.BookId == bookId && x.IsDeleted == false).LastOrDefault();
        }

        public void DeleteBookDrawerByDrawerId(int?[] drawerId)
        {
            var entities = _entities.Where(f => drawerId.Contains(f.DrawerId)).ToList();
            entities.ForEach(a => a.IsDeleted = true);
            _context.SaveChangesAsync();
        }

        public void GetBookDrawerByListBookId(int?[] bookId)
        {
            var entities = _entities.Where(f => bookId.Contains(f.BookId) && f.IsDeleted == false).ToList();
            entities.ForEach(a => a.IsDeleted = true);
        }

        public IEnumerable<BookDrawer> GetBookDrawerByListBook(IEnumerable<BookDto> books)
        {
            List<BookDrawer> bookDrawers = new List<BookDrawer>();
            foreach (var book in books)
            {
                var bookDrawer = _entities.Where(x => x.Id == book.BookDrawerId && x.IsDeleted == false).OrderByDescending(x => x.Time).FirstOrDefault();
                bookDrawers.Add(bookDrawer);
            }
            return bookDrawers;
        }
    }
}
