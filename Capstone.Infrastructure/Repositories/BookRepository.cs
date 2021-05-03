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
        public BookRepository(CapstoneContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<BookDto> GetAllBooks()
        {
            return _entities.Select(c => new BookDto
            {
                Id = c.Id,
                BarCode = c.Barcode,
                PatronImage = c.BorrowDetail.OrderByDescending(x=>x.Id).Where(x=>x.IsReturn == false).FirstOrDefault().Borrow.Patron.Image,
                PatronId = c.BorrowDetail.OrderByDescending(x => x.Id).Where(x => x.IsReturn == false).FirstOrDefault().Borrow.Patron.Id,
                PatronName = c.BorrowDetail.OrderByDescending(x => x.Id).Where(x => x.IsReturn == false).FirstOrDefault().Borrow.Patron.Name,
                BookGroupId = c.BookGroupId,
                DrawerName = c.Drawer.Name,
                Username = c.BorrowDetail.OrderByDescending(x => x.Id).Where(x => x.IsReturn == false).FirstOrDefault().Borrow.Patron.Username,
                BookShelfName = c.Drawer.BookShelf.Name,
                DrawerId = c.DrawerId,             
                IsDeleted = c.IsDeleted,
                BorrowId = c.BorrowDetail.OrderByDescending(x => x.Id).Where(x => x.IsReturn == false).FirstOrDefault().Borrow.Id,
                Note = c.Note,
                IsAvailable = c.IsAvailable,
                LocationName = c.Drawer.BookShelf.Location.Name,
                BookName = (c.BookGroup.Name)
            }).OrderByDescending(x => x.Id).ToList();
        }

        public IEnumerable<BookDto> GetAllBooksInDrawer()
        {
            return _entities.Where(x => x.DrawerId != null && x.IsDeleted == false).Select(c => new BookDto
            {
                Id = c.Id,
                BarCode = c.Barcode,
                BookGroupId = c.BookGroupId,
                IsAvailable = c.IsAvailable,
                BookName = (c.BookGroup.Name)
            }).ToList();
        }

        public IEnumerable<BookDto> GetAllBooksNotInDrawer()
        {
            return _entities.Where(x => x.IsDeleted == false && x.DrawerId == null).Select(c => new BookDto
            {
                Id = c.Id,
                BarCode = c.Barcode,
                BookGroupId = c.BookGroupId,
                IsAvailable = c.IsAvailable,
                BookName = (c.BookGroup.Name)
            }).ToList();
        }

        public IEnumerable<BookDto> GetBookInDrawer()
        {
            return _entities.Where(x => x.IsDeleted == false && x.DrawerId != null).Select(c => new BookDto
            {
                Id = c.Id,
                BarCode = c.Barcode,
                BookGroupId = c.BookGroupId,
                IsAvailable = c.IsAvailable,
                BookName = (c.BookGroup.Name)
            }).ToList();
        }

        public int?[] GetBookIdInDrawer(int?[] drawerId)
        {
            List<int?> termsList = new List<int?>();
            var entites = _entities.Where(f => drawerId.Contains(f.DrawerId)).ToList();
            foreach (var entity in entites)
            {
                termsList.Add(entity.Id);
            }
            int?[] terms = termsList.ToArray();
            //for (int i = 0; i < terms.Length; i++)
            //{
            //    var book = _entities.Where(x => x.Id == terms[i]).FirstOrDefault();
            //    book.DrawerId = null;
            //}
            return terms;
        }


        public IEnumerable<BookDto> GetBookByBookGroup(int? bookGroupId)
        {
            return _entities.Where(x => x.BookGroupId == bookGroupId)
                .Select(c => new BookDto
                {
                    Id = c.Id,
                    BarCode = c.Barcode,
                    BookGroupId = c.BookGroupId,
                    IsAvailable = c.IsAvailable,
                    DrawerId = c.Drawer.Id,
                    BookName = (c.BookGroup.Name)
                }).ToList();
        }

        public BookDto GetBookByBookId(int? bookId)
        {
            return _entities.Where(x => x.Id == bookId && x.IsDeleted == false)
                .Select(c => new BookDto
                {
                    Id = c.Id,
                    BarCode = c.Barcode,
                    BookGroupId = c.BookGroupId,
                    IsAvailable = c.IsAvailable,
                    BookShelfName = c.Drawer.BookShelf.Name,
                    DrawerBarCode = c.Drawer.Barcode,
                    DrawerId = c.Drawer.Id,
                    BookName = (c.BookGroup.Name)
                }).FirstOrDefault();
        }

        public IEnumerable<BookDto> GetBookByDrawer(int? drawerId)
        {
            return _entities.Where(x => x.DrawerId == drawerId && x.IsDeleted == false)
                .Select(c => new BookDto
                {
                    Id = c.Id,
                    BarCode = c.Barcode,
                    BookGroupId = c.BookGroupId,
                    IsAvailable = c.IsAvailable,
                    BookName = (c.BookGroup.Name)
                }).ToList();
        }

        public IEnumerable<BookDto> GetBookByBarcode(string[] barCode)
        {
            var entities = _entities.Where(f => barCode.Contains(f.Barcode)).Select(c => new BookDto
            {
                Id = c.Id,
                BarCode = c.Barcode,
                BookGroupId = c.BookGroupId,
                IsDeleted  = c.IsDeleted,
                IsAvailable = c.IsAvailable,
                DrawerId = c.DrawerId,
                BookShelfName = c.Drawer.BookShelf.Name,
                BookName = (c.BookGroup.Name)
            }).ToList();
            return entities;
        }

        public BookDto GetBookByBookId(int bookId)
        {
            var entities = _entities.Where(c => c.Id == bookId).Select(c => new BookDto
            {
                Id = c.Id,
                BarCode = c.Barcode,
                BookGroupId = c.BookGroupId,
                IsAvailable = c.IsAvailable,
                DrawerId = c.DrawerId,
                BookName = (c.BookGroup.Name)
            }).FirstOrDefault();
            return entities;
        }
    }
}
