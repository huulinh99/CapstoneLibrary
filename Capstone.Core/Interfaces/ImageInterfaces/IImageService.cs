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
        Task<Image> GetImage(int id);
        Task InsertImage(Image image);
        Task<bool> UpdateImage(Image image);
        Task<bool> DeleteImage(int?[] id);
    }
}
