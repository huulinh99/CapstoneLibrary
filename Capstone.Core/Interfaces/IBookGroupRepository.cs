﻿using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IBookGroupRepository : IRepository<BookGroup>
    {
        Task<IEnumerable<BookGroup>> GetBookGroupsByName(string bookGroupName);
    }
}

