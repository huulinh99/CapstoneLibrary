﻿using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IBookShelfRepository : IRepository<BookShelf>
    {
        IEnumerable<BookShelfDto> GetBookShelvesByLocation(int locationId);
        IEnumerable<BookShelfDto> GetBookShelvesAndLocationName();
        void DeleteBookShelfInLocation(int?[] locationId);
        int?[] GetBookShelfIdInLocation(int?[] locationId);
        IEnumerable<BookShelfDto> GetBookShelvesByDrawer(IEnumerable<DrawerDto> drawers);
    }
}
