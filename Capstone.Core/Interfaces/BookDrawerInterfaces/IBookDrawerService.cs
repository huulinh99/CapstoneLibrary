﻿using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces.BookDrawerInterfaces
{
    public interface IBookDrawerService
    {
        PagedList<BookDrawer> GetBookDrawers(BookDrawerQueryFilter filters);
        BookDrawer GetBookDrawer(int id);
        void InsertBookDrawer(BookDrawer bookDrawer);
        bool UpdateBookDrawer(BookDrawer bookDrawer);
        bool DeleteBookDrawer(int?[] id);
    }
}
