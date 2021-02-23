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
        Campaign GetCampaign(int id);
        void InsertCampaign(Campaign campaign);
        bool UpdateCampaign(Campaign campaign);
        bool DeleteCampaign(int?[] id);
    }
}
