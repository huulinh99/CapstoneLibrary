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
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public RoleService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteRole(int?[] id)
        {
            await _unitOfWork.RoleRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Role> GetRole(int id)
        {
            return await _unitOfWork.RoleRepository.GetById(id);
        }

        public PagedList<Role> GetRoles(RoleQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var roles = _unitOfWork.RoleRepository.GetAll();
            if (filters.Name != null)
            {
                roles = roles.Where(x => x.Name == filters.Name);
            }
            var pagedRoles = PagedList<Role>.Create(roles, filters.PageNumber, filters.PageSize);
            return pagedRoles;
        }

        public async Task InsertRole(Role role)
        {
            await _unitOfWork.RoleRepository.Add(role);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateRole(Role role)
        {
            _unitOfWork.RoleRepository.Update(role);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
