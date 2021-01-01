using Capstone.Core.Entities;
using Capstone.Core.Interfaces.ImageInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        IBookGroupRepository BookGroupRepository { get; }
        ILocationRepository LocationRepository { get; }
        IBookShelfRepository BookShelfRepository { get; }
        IDrawerRepository DrawerRepository { get; }
        IErrorMessageRepository ErrorMessageRepository { get; }
        IStaffRepository StaffRepository { get; }
        IBorrowBookRepository BorrowBookRepository { get; }
        IRoleRepository RoleRepository { get; }
        IBorrowDetailRepository BorrowDetailRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICampaignRepository CampaignRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IReturnBookRepository ReturnBookRepository { get; }
        IReturnDetailRepository ReturnDetailRepository { get; }
        IBookCategoryRepository BookCategoryRepository { get; }
        IBookDetectRepository BookDetectRepository { get; }
        IBookRecommendRepository BookRecommendRepository { get; }
        IDeviceRepository DeviceRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IImageRepository ImageRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
