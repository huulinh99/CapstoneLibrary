using Capstone.Core.Entities;
using Capstone.Core.Interfaces.DetectionErrorInterfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class DetectionErrorRepository : BaseRepository<DetectionError>, IDetectionErrorRepository
    {
        public DetectionErrorRepository(CapstoneContext context) : base(context) { }
    }
}
