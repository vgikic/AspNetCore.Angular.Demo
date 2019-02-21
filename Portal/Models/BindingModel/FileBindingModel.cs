using System.ComponentModel.DataAnnotations;

namespace Angular.Core.Models.BindingModel
{
    public class FileBindingModel
    {
        public byte[] Bytes { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
