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
    public class StaffService : IStaffService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public StaffService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<StaffDto> GetLoginByCredenticals(UserLogin login)
        {
            return await _unitOfWork.StaffRepository.GetLoginByCredentials(login);
        }
        public async Task<bool> DeleteStaff(int?[] id)
        {
            await _unitOfWork.StaffRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Staff> GetStaff(int id)
        {
            return await _unitOfWork.StaffRepository.GetById(id);
        }

        public async Task<Staff> GetStaffByUserName(string username)
        {
            return await _unitOfWork.StaffRepository.GetStaffByUsername(username);
        }

        public PagedList<Staff> GetStaffs(StaffQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var staffs = _unitOfWork.StaffRepository.GetAll();
            if (filters.Name != null)
            {
                staffs = staffs.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }
            var pagedStaffs = PagedList<Staff>.Create(staffs, filters.PageNumber, filters.PageSize);
            return pagedStaffs;
        }

        public async Task InsertStaff(Staff staff)
        {
            staff.RoleId = 1;
            await _unitOfWork.StaffRepository.Add(staff);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateStaff(Staff staff)
        {
            staff.IsDeleted = false;
            staff.RoleId = 1;
            _unitOfWork.StaffRepository.Update(staff);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
