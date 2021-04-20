using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class ReturnDetailRepository : BaseRepository<ReturnDetail>, IReturnDetailRepository
    {
        public ReturnDetailRepository(CapstoneContext context) : base(context) { }

        public ReturnDetailDto GetPatronByBookId(int? bookId)
        {
            var entities = _entities.Where(c => c.BookId == bookId).OrderBy(c => c.Id).Select(c => new ReturnDetailDto
            {
                Id = c.Id,
                ReturnId = c.ReturnId,
                BookId = c.BookId,
                PatronId = c.Return.PatronId
            }).LastOrDefault();
            return entities;
        }

        public IEnumerable<ReturnDetailDto> GetAllReturnDetailWithBookName()
        {
            return _entities.Where(x => x.IsDeleted == false).Select(c => new ReturnDetailDto
            {
                Id = c.Id,
                BookId = c.BookId,
                BookName = c.Book.BookGroup.Name,
                Author = c.Book.BookGroup.Author,
                BookGroupId = c.Book.BookGroupId,
                PatronId = c.Return.PatronId,
                Fee = c.Fee,
                PunishFee = c.PunishFee,
                Image = c.Book.BookGroup.Image.Where(x => x.IsDeleted == false).FirstOrDefault().Url,
                IsLate = c.IsLate,
                ReturnId = c.ReturnId,
                ReturnTime = c.Return.ReturnTime
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
