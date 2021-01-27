using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Interfaces.ImageInterfaces
{
    public interface IImageRepository : IRepository<Image>
    {
        IEnumerable<Image> GetImageByBookGroupId(int? bookGroupId);
    }
}
