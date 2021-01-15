﻿using Capstone.Core.DTOs;
using Capstone.Core.Entities;
using Capstone.Core.Enumerations;
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
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
    {
        public StaffRepository(CapstoneContext context) : base(context) { }

        public async Task<IEnumerable<Staff>> GetStaffsByName(string name)
        {
            return await _entities.Where(x => x.Name == name && x.IsDeleted == false).ToListAsync();
        }

        public async Task<StaffDto> GetLoginByCredentials(UserLogin login)
        {
            return await _entities.Where(x => x.Username == login.Username 
            && x.Password == login.Password
            && x.IsDeleted == false)
                .Include(c=> c.Role)
                .Select(c => new StaffDto
            {
                Id = c.Id,
                Name = c.Name,
                Username = c.Username,
                Password = c.Password,
                Address = c.Address,
                DoB = c.DoB,
                Email = c.Email,
                Gender = c.Gender,
                Phone = c.Phone,
                Role = c.Role.Name
            }).FirstOrDefaultAsync();
        }
       
    }
}
