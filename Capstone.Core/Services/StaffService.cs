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

        public StaffDto GetLoginByCredenticalsStaff(UserLogin login)
        {
            return _unitOfWork.StaffRepository.GetLoginByCredentials(login);
        }
        public bool DeleteStaff(int?[] id)
        {
            _unitOfWork.StaffRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public Staff GetStaff(int id)
        {
            return _unitOfWork.StaffRepository.GetById(id);
        }

        public Staff GetStaffByUserName(string username)
        {
            return _unitOfWork.StaffRepository.GetStaffByUsername(username);
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
            if(filters.Username != null)
            {
                staffs = staffs.Where(x => x.Username.ToLower()==filters.Username.ToLower());
            }
            if (filters.Email != null)
            {
                staffs = staffs.Where(x => x.Email.ToLower()==filters.Email.ToLower());
            }
            var pagedStaffs = PagedList<Staff>.Create(staffs, filters.PageNumber, filters.PageSize);
            return pagedStaffs;
        }

        public void InsertStaff(Staff staff)
        {
            staff.RoleId = 1;
            _unitOfWork.StaffRepository.Add(staff);
            _unitOfWork.SaveChanges();
        }

        public bool UpdateStaff(Staff staff)
        {
            staff.IsDeleted = false;
            staff.RoleId = 1;
            staff.CreatedTime = DateTime.Now;
            _unitOfWork.StaffRepository.Update(staff);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
