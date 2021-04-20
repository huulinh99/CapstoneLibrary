using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core.Interfaces
{
    public interface IPatronRepository : IRepository<Patron>
    {
        //Task<patron> GetpatronById(int? id);
        Patron GetPatronByEmail(string email);
        PatronDto GetLoginByCredentials(UserLogin login);
    }
}
