using Capstone.Core.Entities;
using Capstone.Core.Interfaces.FavouriteCategoryInterfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class FavouriteCategoryRepository : BaseRepository<FavouriteCategory>, IFavouriteCategoryRepository
    {
        public FavouriteCategoryRepository(CapstoneContext context) : base(context) { }
    }
}
