using System;
using System.Collections.Generic;

namespace Angular.Core.Models.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsActive { get; set; }
        public int? CategoryId { get; set; }
        public int OptionId { get; set; } = 1;
        public IEnumerable<int> Parts { get; set; }

        public override string ToString() => $"{Id}, {Name}, {Email}";
    }
}
