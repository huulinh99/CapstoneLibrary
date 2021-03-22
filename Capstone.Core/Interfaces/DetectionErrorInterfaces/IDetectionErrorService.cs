using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces.DetectionErrorInterfaces
{
    public interface IDetectionErrorService
    {
        IEnumerable<DetectionErrorDto> GetDetectionErrors(DetectionErrorQueryFilter filters);
        DetectionError GetDetectionError(int id);
        void InsertDetectionError(DetectionError detectionError);
        bool UpdateDetectionError(DetectionError detectionError);
        bool DeleteDetectionError(int?[] id);
    }
}
