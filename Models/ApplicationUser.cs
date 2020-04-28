using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace RoskhTest.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Package> Packages { get; set; }
    }
}