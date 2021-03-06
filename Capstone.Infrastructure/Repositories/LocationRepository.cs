﻿using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(CapstoneContext context) : base(context) { }

        public IEnumerable<Location> GetLocationByLocationName(string name)
        {
            return _entities.Where(x => x.Name == name && x.IsDeleted == false).ToList();
        }
    }
}
