using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class CampaignRepository : BaseRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(CapstoneContext context) : base(context) { }
        public async Task<IEnumerable<Campaign>> GetCampaignsByStaffId(int staffId)
        {
            return await _entities.Where(x => x.StaffId == staffId && x.IsDeleted == false).ToListAsync();
        }
    }
}
