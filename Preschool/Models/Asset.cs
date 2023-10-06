using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Asset
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string Caption { get; set; }

        [Display(Name = "Registered At")]
        [DataType(DataType.Date)]
        public DateTime RegisterdAt { get; set; }
    }
}
