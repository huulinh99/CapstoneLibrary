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
    public class CampaignService : ICampaignService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public CampaignService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteCampaign(int?[] id)
        {
            await _unitOfWork.CampaignRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Campaign> GetCampaign(int id)
        {
            return await _unitOfWork.CampaignRepository.GetById(id);
        }

        public PagedList<Campaign> GetCampaigns(CampaignQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var campaigns = _unitOfWork.CampaignRepository.GetAll();
            if (filters.StaffId != null)
            {
                campaigns = campaigns.Where(x => x.StaffId == filters.StaffId);
            }
            var pagedCampaigns = PagedList<Campaign>.Create(campaigns, filters.PageNumber, filters.PageSize);
            return pagedCampaigns;
        }

        public async Task InsertCampaign(Campaign campaign)
        {
            await _unitOfWork.CampaignRepository.Add(campaign);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateCampaign(Campaign campaign)
        {
            _unitOfWork.CampaignRepository.Update(campaign);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
