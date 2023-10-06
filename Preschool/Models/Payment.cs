using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Payment Date field is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "The Amount field is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        // Foreign key to the associated invoice
        public int InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }

    }
}
