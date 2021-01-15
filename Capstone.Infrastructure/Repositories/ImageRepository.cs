using Capstone.Core.Entities;
using Capstone.Core.Interfaces.ImageInterfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public ImageRepository(CapstoneContext context) : base(context) { }
        public  IEnumerable<Image> GetImageByBookGroupId(int? bookGroupId)
        {

            return  _entities.Where(x => x.IsDeleted == false && x.BookGroupId==bookGroupId).ToList();
            
        }
    }
}
