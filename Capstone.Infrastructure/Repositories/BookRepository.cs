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
                BookGroupId = c.BookGroupId,
                IsAvailable = c.IsAvailable,
                BookName = (c.BookGroup.Name)
            }).ToList();
        }

        public IEnumerable<BookDto> GetAllBooksInDrawer()
        {
            return _entities.Where(x => x.DrawerId != null && x.IsDeleted == false).Select(c => new BookDto
            {
                Id = c.Id,
                BarCode = c.BarCode,
                BookGroupId = c.BookGroupId,
                IsAvailable = c.IsAvailable,
                BookName = (c.BookGroup.Name)
            }).ToList();
        }

        public IEnumerable<BookDto> GetAllBooksNotInDrawer()
        {
            return _entities.Where(x => x.IsDeleted == false && x.DrawerId==null).Select(c => new BookDto
            {
                Id = c.Id,
                BarCode = c.BarCode,
                BookGroupId = c.BookGroupId,
                IsAvailable = c.IsAvailable ,
                BookName = (c.BookGroup.Name)
            }).ToList();
        }

        public IEnumerable<BookDto> GetBookInDrawer()
        {
            return _entities.Where(x => x.IsDeleted == false && x.DrawerId != null).Select(c => new BookDto
            {
                Id = c.Id,
                BarCode = c.BarCode,
                BookGroupId = c.BookGroupId,
                IsAvailable = c.IsAvailable,
                BookName = (c.BookGroup.Name)
            }).ToList();
        }

        public IEnumerable<BookDto> GetBookByBookGroup(int? bookGroupId)
        {
            return _entities.Where(x => x.BookGroupId == bookGroupId && x.IsDeleted == false && x.DrawerId !=null)
                .Select(c => new BookDto
                {
                    Id = c.Id,
                    BarCode = c.BarCode,
                    BookGroupId = c.BookGroupId,
                    IsAvailable = c.IsAvailable,
                    DrawerId = c.Drawer.Id,
                    BookName = (c.BookGroup.Name)
                }).ToList();
        }

        public IEnumerable<BookDto> GetBookByDrawer(int? drawerId)
        {
            return _entities.Where(x => x.DrawerId == drawerId && x.IsDeleted == false)
                .Select(c => new BookDto
                {
                    Id = c.Id,
                    BarCode = c.BarCode,
                    BookGroupId = c.BookGroupId,
                    IsAvailable = c.IsAvailable,
                    BookName = (c.BookGroup.Name)
                }).ToList();
        }

        public IEnumerable<BookDto> GetBookByListId(string[] barcode)
        {
            var entities = _entities.Where(f => barcode.Contains(f.BarCode)).Select(c => new BookDto
            {
                Id = c.Id,
                BarCode = c.BarCode,
                BookGroupId = c.BookGroupId,
                IsAvailable = c.IsAvailable,
                DrawerId = c.DrawerId,
                BookName = (c.BookGroup.Name)
            }).ToList();
            return entities;
        }
    }
}
