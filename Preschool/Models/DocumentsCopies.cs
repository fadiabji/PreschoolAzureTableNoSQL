using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Preschool.Models
{
    public class DocumentsCopies
    {
            [Key]
            [Required]
            public int Id { get; set; }

            [Required]
            public string ImageFile { get; set; }

    }
}
