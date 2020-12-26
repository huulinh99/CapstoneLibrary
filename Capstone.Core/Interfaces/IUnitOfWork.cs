using Capstone.Core.Entities;
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

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
