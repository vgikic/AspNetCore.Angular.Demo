using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Angular.Core.Models.BindingModel
{
    public class ItemBindingModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsActive { get; set; }
        public int OptionId { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<int> Parts { get; set; }

        public override string ToString() => $"{Name}, {Email}";

    }
}
