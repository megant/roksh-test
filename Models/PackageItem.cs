using System;

namespace RoskhTest.Models
{
    public partial class PackageItem
    {
        public Guid Id { get; set; }
        public Guid PackageId { get; set; }
        public Package Package { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
    }
}