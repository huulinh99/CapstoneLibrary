using Capstone.Core.CustomEntities;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces.ImageInterfaces
{
    public interface IImageService
    {
        PagedList<Image> GetImages(ImageQueryFilter filters);
        Image GetImage(int id);
        void InsertImage(Image image);
        bool UpdateImage(Image image);
        bool DeleteImage(int?[] id);
    }
}
