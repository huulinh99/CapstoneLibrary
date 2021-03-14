using Capstone.Core.Entities;
using Capstone.Core.Interfaces.DetectionInterfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class DetectionRepository : BaseRepository<Detection>, IDetectionRepository
    {
        public DetectionRepository(CapstoneContext context) : base(context) { }
    }
}
