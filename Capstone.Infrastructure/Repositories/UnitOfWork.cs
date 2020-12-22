using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CapstoneContext _context;
        private readonly IBookRepository _bookRepository;
        public UnitOfWork(CapstoneContext context)
        {
            _context = context;

        }
        public IBookRepository BookRepository => _bookRepository ?? new BookRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
