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
        ILocationRepository LocationRepository { get; }
        IBookShelfRepository BookShelfRepository { get; }
        IDrawerRepository DrawerRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
