using Capstone.Core.Entities;
using Capstone.Core.Interfaces.ImageInterfaces;
using Capstone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Infrastructure.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public ImageRepository(CapstoneContext context) : base(context) { }
    }
}
