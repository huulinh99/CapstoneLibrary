using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces.DetectionErrorInterfaces
{
    public interface IDetectionErrorService
    {
        PagedList<DetectionError> GetDetectionErrors(DetectionErrorQueryFilter filters);
        DetectionError GetDetectionError(int id);
        void InsertDetectionError(DetectionError detectionError);
        bool UpdateDetectionError(DetectionError detectionError);
        bool DeleteDetectionError(int?[] id);
    }
}
