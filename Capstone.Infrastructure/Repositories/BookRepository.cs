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
        public BookRepository(CapstoneContext context) : base(context) { }
        public  IEnumerable<BookDto> GetAllBooksNotInDrawer()
        {
            return  _entities.Where(x => x.BookDrawerId == null && x.IsDeleted == false).Select(c => new BookDto
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
    }
}
