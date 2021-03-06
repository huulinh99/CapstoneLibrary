﻿using Capstone.Core.CustomEntities;
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
        public bool DeleteLocation(int?[] id)
        {
            _unitOfWork.LocationRepository.Delete(id);
            _unitOfWork.BookShelfRepository.DeleteBookShelfInLocation(id);
            var bookShelfId =  _unitOfWork.BookShelfRepository.GetBookShelfIdInLocation(id);
            _unitOfWork.DrawerRepository.DeleteDrawerInBookShelf(bookShelfId.ToArray());
            var drawerId = _unitOfWork.DrawerRepository.GetDrawerIdInBookShelf(bookShelfId.ToArray());
            //_unitOfWork.BookDrawerRepository.DeleteBookDrawerByDrawerId(drawerId.ToArray());
            //var bookDrawerId = _unitOfWork.BookDrawerRepository.GetBookDrawerIdInDrawer(drawerId.ToArray());
            //_unitOfWork.BookRepository.DeleteBookByBookDrawerId(bookDrawerId.ToArray());
            _unitOfWork.SaveChanges();
            return true;
        }

        public Location GetLocation(int id)
        {
            return _unitOfWork.LocationRepository.GetById(id);
        }

        public PagedList<Location> GetLocations(LocationQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var locations = _unitOfWork.LocationRepository.GetAll();
            if (filters.Name != null)
            {
                locations = locations.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }

            if (filters.IsRoom != null)
            {
                locations = locations.Where(x => x.IsRoom == filters.IsRoom);
            }
            var pagedLocations = PagedList<Location>.Create(locations, filters.PageNumber, filters.PageSize);
            return pagedLocations;
        }

        public void InsertLocation(Location location)
        {
            _unitOfWork.LocationRepository.Add(location);
           _unitOfWork.SaveChanges();
        }

        public bool UpdateLocation(Location location)
        {
            location.IsDeleted = false;
            _unitOfWork.LocationRepository.Update(location);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}

