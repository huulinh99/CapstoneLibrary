using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class BorrowDetailRepository : BaseRepository<BorrowDetail>, IBorrowDetailRepository
    {
        public BorrowDetailRepository(CapstoneContext context) : base(context) { }
        public IEnumerable<BorrowDetailDto> GetAllBorrowDetailAndBookName()
        {
            return _entities.Include(c => c.Book).Where(x => x.IsDeleted == false).Select(c => new BorrowDetailDto
            {
                Id = c.Id,
                BookId = c.BookId,
                BookName = c.Book.BookGroup.Name,
                BorrowId = c.BorrowId,
                Author = c.Book.BookGroup.Author,
                Fee = c.Book.BookGroup.Fee,
                Image = c.Book.BookGroup.Image.Where(x => x.IsDeleted == false).FirstOrDefault().Url,
                StartTime = c.Borrow.StartTime,
                ReturnTime = c.Borrow.EndTime,
                PunishFee = c.Book.BookGroup.PunishFee
            }).ToList();
        }
        public IEnumerable<BorrowDetailDto> GetAllBorrowDetail(int? borrowId)
        {
            return _entities.Include(c => c.Book).Where(x => x.IsDeleted == false).Select(c => new BorrowDetailDto
            {
                Id = c.Id,
                BookId = c.BookId,
                BookName = c.Book.BookGroup.Name,
                BorrowId = c.BorrowId,
                Author = c.Book.BookGroup.Author,
                Fee = c.Book.BookGroup.Fee,
                Image = c.Book.BookGroup.Image.Where(x => x.IsDeleted == false).FirstOrDefault().Url,
                StartTime = c.Borrow.StartTime,
                PunishFee = c.Book.BookGroup.PunishFee
            }).ToList();
        }
        public IEnumerable<BorrowDetailDto> GetBorrowDetailWithFee(int? borrowId)
        {
            return _entities.Include(c => c.Book).Where(x => x.IsDeleted == false && x.BorrowId == borrowId).Select(c => new BorrowDetailDto
            {
                Id = c.Id,
                BookId = c.BookId,
                BookName = c.Book.BookGroup.Name,
                BorrowId = c.BorrowId,
                Author = c.Book.BookGroup.Author,
                Fee = c.Book.BookGroup.Fee,
                Image = c.Book.BookGroup.Image.Where(x => x.IsDeleted == false).FirstOrDefault().Url,
                StartTime = c.Borrow.StartTime,
                PunishFee = c.Book.BookGroup.PunishFee
            }).ToList();
        }

        public IEnumerable<BorrowDetailDto> GetBorrowDetailWithCount()
        {
            return _entities.Include(c => c.Book).Where(x => x.IsDeleted == false).GroupBy(x => x.Book.BookGroupId).Select(c => new BorrowDetailDto
            {
                BookId = c.Key,
                Count = c.Count()
            }).ToList();
        }

        public IEnumerable<BorrowDetailDto> GetBorrowDetailWithListBorrow(IEnumerable<BorrowBookDto> borrowBooks)
        {
            List<BorrowDetailDto> borrowDetails = new List<BorrowDetailDto>();
            foreach (var borrowBook in borrowBooks)
            {
                var borrowBookDtos = _entities.Include(c => c.Book).Where(x => x.IsDeleted == false && x.BorrowId == borrowBook.Id).Select(c => new BorrowDetailDto
                {
                    Id = c.Id,
                    BookId = c.BookId,
                    BookName = c.Book.BookGroup.Name,
                    BorrowId = c.BorrowId,
                    Author = c.Book.BookGroup.Author,
                    Fee = c.Book.BookGroup.Fee,
                    Image = c.Book.BookGroup.Image.Where(x => x.IsDeleted == false).FirstOrDefault().Url,
                    StartTime = c.Borrow.StartTime,
                    ReturnTime = c.Borrow.EndTime,
                    PunishFee = c.Book.BookGroup.PunishFee
                }).ToList();
                foreach (var borrowBookDto in borrowBookDtos)
                {
                    borrowDetails.Add(borrowBookDto);
                }
            }
            return borrowDetails;
        }

        public BorrowDetailDto GetCustomerByBookId(int? bookId)
        {
            var entities = _entities.Where(c => c.BookId == bookId).OrderBy(c => c.Id).Select(c => new BorrowDetailDto
            {
                Id = c.Id,
                BorrowId = c.BorrowId,
                BookId = c.BookId,
                CustomerId = c.Borrow.CustomerId
            }).LastOrDefault();
            return entities;
        }
    }
}
