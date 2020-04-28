using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoskhTest.ViewModels;

namespace RoskhTest.Models
{
    public partial class Package
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int? StateId { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual PackageState State { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<PackageItem> PackageItems { get; set; }

        public ViewPackage ToViewPackage()
        {
            return new ViewPackage()
            {
                Id = Id,
                Code = Code,
                Description = Description,
                Name = Name,
                State = State.Description
            };
        }
    }
}
