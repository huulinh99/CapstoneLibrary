using Capstone.Core.CustomEntities;
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

        public async Task<bool> DeleteDrawer(int id)
        {
            await _unitOfWork.DrawerRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Drawer> GetDrawer(int id)
        {
            return await _unitOfWork.DrawerRepository.GetById(id);
        }

        public PagedList<Drawer> GetDrawers(DrawerQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var drawers = _unitOfWork.DrawerRepository.GetAll();
            if (filters.BookSheflId != null)
            {
                drawers = drawers.Where(x => x.BookSheflId == filters.BookSheflId);
            }
            var pagedDrawers = PagedList<Drawer>.Create(drawers, filters.PageNumber, filters.PageSize);
            return pagedDrawers;
        }

        public async Task InsertDrawer(Drawer drawer)
        {
            await _unitOfWork.DrawerRepository.Add(drawer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateDrawer(Drawer drawer)
        {
            _unitOfWork.DrawerRepository.Update(drawer);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
