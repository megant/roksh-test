using System;
using System.Collections.Generic;

namespace RoskhTest.Models
{
    public partial class PackageState
    {
        public PackageState()
        {
            Packages = new HashSet<Package>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Package> Packages { get; set; }
    }

    public enum DeliveryState
    {
        WfPU = 1,
        PU = 2,
        ID = 3,
        OD = 4,
        DD = 5
    }
}
