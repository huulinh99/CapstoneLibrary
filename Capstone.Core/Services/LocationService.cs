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
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public LocationService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteLocation(int[] id)
        {
            await _unitOfWork.LocationRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Location> GetLocation(int id)
        {
            return await _unitOfWork.LocationRepository.GetById(id);
        }

        public PagedList<Location> GetLocations(LocationQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var locations = _unitOfWork.LocationRepository.GetAll();
            if (filters.Name != null)
            {
                locations = locations.Where(x => x.Name.Contains(filters.Name));
            }
            var pagedLocations = PagedList<Location>.Create(locations, filters.PageNumber, filters.PageSize);
            return pagedLocations;
        }

        public async Task InsertLocation(Location location)
        {
            await _unitOfWork.LocationRepository.Add(location);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateLocation(Location location)
        {
            _unitOfWork.LocationRepository.Update(location);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}

