using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Services
{
    public class DrawerService : IDrawerService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public DrawerService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public bool DeleteDrawer(int?[] id)
        {
            _unitOfWork.DrawerRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public Drawer GetDrawer(int id)
        {
            return _unitOfWork.DrawerRepository.GetById(id);
        }

        public PagedList<DrawerDto> GetDrawers(DrawerQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var drawers = _unitOfWork.DrawerRepository.GetAllDrawers(filters.BookSheflId, filters.RowStart, filters.RowEnd, filters.ColStart, filters.ColEnd);
            if (filters.BookGroupId != null)
            {
                var books = _unitOfWork.BookRepository.GetBookByBookGroup(filters.BookGroupId);
                drawers = _unitOfWork.DrawerRepository.GetDrawerByListBook(books);
            }
            var pagedDrawers = PagedList<DrawerDto>.Create(drawers, filters.PageNumber, filters.PageSize);
            return pagedDrawers;
        }

        public void InsertDrawer(Drawer drawer)
        {
            _unitOfWork.DrawerRepository.Add(drawer);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateDrawer(Drawer drawer)
        {
            _unitOfWork.DrawerRepository.Update(drawer);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
