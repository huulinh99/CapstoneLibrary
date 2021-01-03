using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface ICampaignService
    {
        PagedList<Campaign> GetCampaigns(CampaignQueryFilter filters);
        Task<Campaign> GetCampaign(int id);
        Task InsertCampaign(Campaign campaign);
        Task<bool> UpdateCampaign(Campaign campaign);
        Task<bool> DeleteCampaign(int[] id);
    }
}
