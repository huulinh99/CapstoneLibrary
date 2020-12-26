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
        private readonly IBookGroupRepository _bookGroupRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IBookShelfRepository _bookShelfRepository;
        private readonly IDrawerRepository _drawerRepository;
        private readonly IErrorMessageRepository _errorMessageRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IBorrowBookRepository _borrowBookRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IBorrowDetailRepository _borrowDetailRepository;
        public UnitOfWork(CapstoneContext context)
        {
            _context = context;

        }
        public IBookRepository BookRepository => _bookRepository ?? new BookRepository(_context);
        public IBookGroupRepository BookGroupRepository => _bookGroupRepository ?? new BookGroupRepository(_context);
        public ILocationRepository LocationRepository => _locationRepository ?? new LocationRepository(_context);
        public IBookShelfRepository BookShelfRepository => _bookShelfRepository ?? new BookShelfRepository(_context);
        public IDrawerRepository DrawerRepository => _drawerRepository ?? new DrawerRepository(_context);
        public IErrorMessageRepository ErrorMessageRepository => _errorMessageRepository ?? new ErrorMessageRepository(_context);
        public IBorrowBookRepository BorrowBookRepository => _borrowBookRepository ?? new BorrowBookRepository(_context);
        public IStaffRepository StaffRepository => _staffRepository ?? new StaffRepository(_context);
        public IRoleRepository RoleRepository => _roleRepository ?? new RoleRepository(_context);
        public IBorrowDetailRepository BorrowDetailRepository => _borrowDetailRepository ?? new BorrowDetailRepository(_context);

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
