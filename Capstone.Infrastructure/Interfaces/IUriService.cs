using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Infrastructure.Services
{
    public interface IUriService
    {
        Uri GetBookPaginationUri(BookQueryFilter filter, string actionUrl);
    }
}
