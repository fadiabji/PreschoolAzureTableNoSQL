using Microsoft.EntityFrameworkCore.Query.Internal;
using Preschool.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Preschool.Models
{
    public class Invoice
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Invoice Number field is required.")]
        public string InvoiceNumber { get; set; } = new Random().Next(1000000, 9999999).ToString();


        [DataType(DataType.Date)]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        // Optional discount field
        [Display(Name = "Discount")]
        [Range(0, 1, ErrorMessage = "Discount must be between 0 and 1.")]
        public decimal? Discount { get; set; }

        // Navigation property for related invoice items
        public virtual ICollection<InvoiceSubscriptionType> InvoiceSubscriptionType { get; set; }

        // Collection of payments made towards this invoice
        public virtual ICollection<Payment> Payments { get; set; }

        [Required]
        public int ChildId { get; set; }
        public virtual Child Child { get; set; }

        public string LogoFileAddress { get; set; }
        public string QRCodeFileAddress { get; set; }


        [NotMapped]
        public decimal CalculateTotal { get; set; }
        [NotMapped]
        public decimal CalculatePayments { get; set; }
        [NotMapped]
        public decimal CalculateBalance { get; set; }



        public Invoice()
        {
            //SubscriptionTypes = new List<SubscriptionType>();
        }

    }

}


