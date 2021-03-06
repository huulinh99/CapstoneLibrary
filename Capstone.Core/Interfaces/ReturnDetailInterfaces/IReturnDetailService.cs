﻿using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IReturnDetailService
    {
        PagedList<ReturnDetail> GetReturnDetails(ReturnDetailQueryFilter filters);
        ReturnDetail GetReturnDetail(int id);
        void InsertReturnDetail(ReturnDetail returnDetail);
        bool UpdateReturnDetail(ReturnDetail returnDetail);
        bool DeleteReturnDetail(int?[] id);
    }
}
