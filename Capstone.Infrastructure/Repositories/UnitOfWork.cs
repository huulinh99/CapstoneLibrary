using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.Interfaces.DetectionErrorInterfaces;
using Capstone.Core.Interfaces.DetectionInterfaces;
using Capstone.Core.Interfaces.DrawerDetectionInterfaces;
using Capstone.Core.Interfaces.FavouriteCategoryInterfaces;
using Capstone.Core.Interfaces.ImageInterfaces;
using Capstone.Core.Interfaces.UndefinedErrorInterfaces;
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
        private readonly IStaffRepository _staffRepository;
        private readonly IBorrowBookRepository _borrowBookRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IBorrowDetailRepository _borrowDetailRepository;
        private readonly IDetectionRepository _detectionRepository;
        private readonly IDetectionErrorRepository _detectionErrorRepository;
        private readonly IUndefinedErrorRepository _undefinedErrorRepository;
        private readonly IDrawerDetectionRepository _drawerDetectionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IReturnBookRepository _returnBookRepository;
        private readonly IReturnDetailRepository _returnDetailRepository;
        private readonly IBookCategoryRepository _bookCategoryRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IUserNotificationRepository _notificationRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IFavouriteCategoryRepository _favouriteCategoryRepository;
        public UnitOfWork(CapstoneContext context)
        {
            _context = context;

        }
        public IBookRepository BookRepository => _bookRepository ?? new BookRepository(_context);
        public IBookGroupRepository BookGroupRepository => _bookGroupRepository ?? new BookGroupRepository(_context);
        public ILocationRepository LocationRepository => _locationRepository ?? new LocationRepository(_context);
        public IBookShelfRepository BookShelfRepository => _bookShelfRepository ?? new BookShelfRepository(_context);
        public IDrawerRepository DrawerRepository => _drawerRepository ?? new DrawerRepository(_context);
        public IBorrowBookRepository BorrowBookRepository => _borrowBookRepository ?? new BorrowBookRepository(_context);
        public IStaffRepository StaffRepository => _staffRepository ?? new StaffRepository(_context);
        public IRoleRepository RoleRepository => _roleRepository ?? new RoleRepository(_context);
        public IDetectionRepository DetectionRepository => _detectionRepository ?? new DetectionRepository(_context);
        public IDrawerDetectionRepository DrawerDetectionRepository => _drawerDetectionRepository ?? new DrawerDetectionRepository(_context);
        public IDetectionErrorRepository DetectionErrorRepository => _detectionErrorRepository ?? new DetectionErrorRepository(_context);
        public IUndefinedErrorRepository UndefinedErrorRepository => _undefinedErrorRepository ?? new UndefinedErrorRepository(_context);
        public IBorrowDetailRepository BorrowDetailRepository => _borrowDetailRepository ?? new BorrowDetailRepository(_context);

        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_context);

        public ICustomerRepository CustomerRepository => _customerRepository ?? new CustomerRepository(_context);
        public IReturnBookRepository ReturnBookRepository => _returnBookRepository ?? new ReturnBookRepository(_context);
        public IReturnDetailRepository ReturnDetailRepository => _returnDetailRepository ?? new ReturnDetailRepository(_context);
        public IBookCategoryRepository BookCategoryRepository => _bookCategoryRepository ?? new BookCategoryRepository(_context);
        public IFeedbackRepository FeedbackRepository => _feedbackRepository ?? new FeedbackRepository(_context);
        public IUserNotificationRepository NotificationRepository => _notificationRepository ?? new UserNotificationRepository(_context);
        public IImageRepository ImageRepository => _imageRepository ?? new ImageRepository(_context);
        public IFavouriteCategoryRepository FavouriteCategoryRepository => _favouriteCategoryRepository ?? new FavouriteCategoryRepository(_context);

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
