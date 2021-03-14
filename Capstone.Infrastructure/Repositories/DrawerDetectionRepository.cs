using Capstone.Core.Entities;
using Capstone.Core.Interfaces.DrawerDetectionInterfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class DrawerDetectionRepository : BaseRepository<DrawerDetection>, IDrawerDetectionRepository
    {
        public DrawerDetectionRepository(CapstoneContext context) : base(context) { }
    }
}
