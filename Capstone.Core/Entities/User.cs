using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string RoleName { get; set; }
        public int? RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string DeviceToken { get; set; }
        public DateTime? DoB { get; set; }         
    }
}
