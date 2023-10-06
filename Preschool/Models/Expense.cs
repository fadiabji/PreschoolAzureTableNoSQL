using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Expense
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter an amount.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid amount.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }

        [Display(Name = "Registered At")]
        [DataType(DataType.Date)]
        public DateTime RegisterdAt { get; set; }
        [Required]
        public string BuyerEmail { get; set; }
    }
}
