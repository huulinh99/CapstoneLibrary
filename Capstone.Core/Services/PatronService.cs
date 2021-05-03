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
    public class PatronService : IPatronService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public PatronService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public bool DeletePatron(int?[] id)
        {
            _unitOfWork.PatronRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public PatronDto GetLoginByCredenticalsPatron(UserLogin login)
        {
            return _unitOfWork.PatronRepository.GetLoginByCredentials(login);
        }
        public Patron GetPatron(int id)
        {
            return _unitOfWork.PatronRepository.GetById(id);
        }

        public Patron GetPatron(string email)
        {
            return _unitOfWork.PatronRepository.GetPatronByEmail(email);
        }
        public PagedList<Patron> GetPatrons(PatronQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var patrons = _unitOfWork.PatronRepository.GetAll();
            if (filters.Name != null)
            {
                patrons = patrons.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }

            if(filters.IsNewest == true)
            {
                patrons = patrons.OrderByDescending(x => x.Id).Take(5);
            }

            if (filters.Username != null)
            {
                patrons = patrons.Where(x => x.Username.ToLower().Contains(filters.Username.ToLower()));
            }

            if (filters.Email != null)
            {
                patrons = patrons.Where(x => x.Email.ToLower().Contains(filters.Email.ToLower()));
            }

            
            var pagedpatrons = PagedList<Patron>.Create(patrons, filters.PageNumber, filters.PageSize);
            return pagedpatrons;
        }

        public void InsertPatron(Patron patron)
        {
            patron.IsDeleted = false;
            patron.RoleId = 2;
            patron.CreatedTime = DateTime.UtcNow;
            _unitOfWork.PatronRepository.Add(patron);
            _unitOfWork.SaveChanges();
        }

        public bool UpdatePatron(Patron patron)
        {
            _unitOfWork.PatronRepository.Update(patron);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
