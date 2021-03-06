﻿using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces.DetectionInterfaces
{
    public interface IDetectionService
    {
        PagedList<Detection> GetDetections(DetectionQueryFilter filters);
        Detection GetDetection(int id);
        void InsertDetection(Detection detection);
        bool UpdateDetection(Detection detection);
        bool DeleteDetection(int?[] id);
    }
}
