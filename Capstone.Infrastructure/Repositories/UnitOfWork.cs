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
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IReturnBookRepository _returnBookRepository;
        private readonly IReturnDetailRepository _returnDetailRepository;
        private readonly IBookCategoryRepository _bookCategoryRepository;
        private readonly IBookDetectRepository _bookDetectRepository;
        private readonly IBookRecommendRepository _bookRecommendRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly INotificationRepository _notificationRepository;
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

        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_context);

        public ICampaignRepository CampaignRepository => _campaignRepository ?? new CampaignRepository(_context);
        public ICustomerRepository CustomerRepository => _customerRepository ?? new CustomerRepository(_context);
        public IReturnBookRepository ReturnBookRepository => _returnBookRepository ?? new ReturnBookRepository(_context);
        public IReturnDetailRepository ReturnDetailRepository => _returnDetailRepository ?? new ReturnDetailRepository(_context);
        public IBookCategoryRepository BookCategoryRepository => _bookCategoryRepository ?? new BookCategoryRepository(_context);
        public IBookDetectRepository BookDetectRepository => _bookDetectRepository ?? new BookDetectRepository(_context);
        public IBookRecommendRepository BookRecommendRepository => _bookRecommendRepository ?? new BookRecommendRepository(_context);
        public IDeviceRepository DeviceRepository => _deviceRepository ?? new DeviceRepository(_context);
        public IFeedbackRepository FeedbackRepository => _feedbackRepository ?? new FeedbackRepository(_context);
        public INotificationRepository NotificationRepository => _notificationRepository ?? new NotificationRepository(_context);

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
