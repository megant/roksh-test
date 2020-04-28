using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fare;
using RoskhTest.ViewModels;
using WebsiteParser.Attributes;
using WebsiteParser.Attributes.Enums;
using WebsiteParser.Attributes.StartAttributes;

namespace RoskhTest.Models
{
    [ListSelector("#product-list", ChildSelector = ".product-box")]
    public partial class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Selector("[data-compare-image]", Attribute = "data-compare-image")]
        public string Code { get; set; }
        [Selector(".name")]
        public string Name { get; set; }
        [Selector(".description")]
        [Remove("(.*) Leírás: ", RemoverValueType.Regex)]
        public string Description { get; set; }
        [Selector(".img-responsive", Attribute = "src", SkipIfNotFound = true)]
        public string ImageUrl { get; set; }
        public virtual ICollection<PackageItem> PackageItems { get; set; }
        
        public ViewItem ToViewItem()
        {
            return new ViewItem()
            {
                Code = Code,
                Description = Description,
                Name = Name,
                ImageUrl = ImageUrl
            };
        }
    }
}
