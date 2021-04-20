using Capstone.Core.CustomEntities;
using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IPatronService
    {
        PagedList<Patron> GetPatrons(PatronQueryFilter filters);
        Patron GetPatron(int id);
        Patron GetPatron(string email);
        void InsertPatron(Patron patron);
        bool UpdatePatron(Patron patron);
        bool DeletePatron(int?[] id);
        PatronDto GetLoginByCredenticalsPatron(UserLogin login);
    }
}
