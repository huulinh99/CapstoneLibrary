using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Capstone.Core.Hubs
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Messages = new HashSet<Message>();
        }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
