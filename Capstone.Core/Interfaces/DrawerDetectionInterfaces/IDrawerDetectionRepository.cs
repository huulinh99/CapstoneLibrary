using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces.DrawerDetectionInterfaces
{
    public interface IDrawerDetectionRepository : IRepository<DrawerDetection>
    {
        IEnumerable<DrawerDetectionDto> GetAllDrawerDetection();
    }
}
