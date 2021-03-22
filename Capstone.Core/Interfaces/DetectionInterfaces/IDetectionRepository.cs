using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces.DetectionInterfaces
{
    public interface IDetectionRepository : IRepository<Detection>
    {
        IEnumerable<DetectionDto> GetAllDetection();
    }
}
