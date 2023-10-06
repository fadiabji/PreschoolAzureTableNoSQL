using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Report
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
