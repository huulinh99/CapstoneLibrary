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
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly CapstoneContext _context;
        public BookRepository(CapstoneContext context) : base(context) {
            _context = context;
        }
        public IEnumerable<BookDto> GetAllBooks()
        {
            return _entities.Where(x =>x.IsDeleted == false).Select(c => new BookDto
            {
                Id = c.Id,
                BarCode = c.BarCode,
                BookDrawerId = c.BookDrawerId,
                BookGroupId = c.BookGroupId,
                Status = c.Status,
                BookName = (c.BookGroup.Name)
            }).ToList();
        }

        public IEnumerable<BookDto> GetAllBooksInDrawer()
        {
            return _entities.Where(x => x.BookDrawerId != null && x.IsDeleted == false).Select(c => new BookDto
            {
                Id = c.Id,
                BarCode = c.BarCode,
                BookDrawerId = c.BookDrawerId,
                BookGroupId = c.BookGroupId,
                Status = c.Status,
                BookName = (c.BookGroup.Name)
            }).ToList();
        }

        public IEnumerable<BookDto> GetAllBooksNotInDrawer()
        {
            return _entities.Where(x => x.BookDrawerId == null && x.IsDeleted == false).Select(c => new BookDto
            {
                Id = c.Id,
                BarCode = c.BarCode,
                BookDrawerId = c.BookDrawerId,
                BookGroupId = c.BookGroupId,
                Status = c.Status,
                BookName = (c.BookGroup.Name)
            }).ToList();
        }

        public IEnumerable<BookDto> GetBookInDrawer(IEnumerable<BookDrawer> bookDrawers)
        {
            List<BookDto> books = new List<BookDto>();
            foreach (var bookDrawer in bookDrawers)
            {
                var book = _entities.Where(x => x.Id == bookDrawer.BookId && x.IsDeleted == false)
                    .Select(c => new BookDto
                    {
                        Id = c.Id,
                        BarCode = c.BarCode,
                        BookDrawerId = c.BookDrawerId,
                        BookGroupId = c.BookGroupId,
                        Status = c.Status,
                        BookName = (c.BookGroup.Name)
                    }).FirstOrDefault();
                books.Add(book);
            }
            return books;
        }

        public IEnumerable<BookDto> GetBookByBookGroup(int? bookGroupId)
        {
            return _entities.Where(x => x.BookGroupId == bookGroupId && x.IsDeleted == false && x.BookDrawerId !=null)
                .Select(c => new BookDto
                {
                    Id = c.Id,
                    BarCode = c.BarCode,
                    BookDrawerId = c.BookDrawerId,
                    BookGroupId = c.BookGroupId,
                    Status = c.Status,
                    BookName = (c.BookGroup.Name)
                }).ToList();
        }

        public void GetBookByBookDrawerId(int?[] bookId)
        {
            var entities = _entities.Where(f => bookId.Contains(f.Id)).ToList();
            entities.ForEach(a => a.BookDrawerId = null);
        }

        public List<IEnumerable<BookDto>> GetBookByBookGroupWithDrawer(IEnumerable<BookGroupDto> bookGroups, IEnumerable<DrawerDto> drawers)
        {
            List<IEnumerable<BookDto>> books = new List<IEnumerable<BookDto>>();
            foreach (var bookGroup in bookGroups)
            {
                foreach (var drawer in drawers)
                {
                    var book = _entities.Where(x => x.BookGroupId == bookGroup.Id && x.IsDeleted == false)
                    .Select(c => new BookDto
                    {
                        Id = c.Id,
                        BarCode = c.BarCode,
                        BookDrawerId = c.BookDrawerId,
                        BookGroupId = c.BookGroupId,
                        Status = c.Status,
                        BookName = (c.BookGroup.Name),
                        Drawer = drawer
                    }).ToList();
                    books.Add(book);
                }
            }
            return books;
        }

        public async Task DeleteBookByBookDrawerId(int?[] bookDrawerId)
        {
            var entities = _entities.Where(f => bookDrawerId.Contains(f.BookDrawerId)).ToList();
            entities.ForEach(a => a.BookDrawerId = null);
            await _context.SaveChangesAsync();
        }
    }
}
