using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;
using System.Composition.Convention;

namespace Preschool.Models
{
    public class SubscriptionType
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Duration Month")]
        public int DurationMonth { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<InvoiceSubscriptionType> InvoiceSubscriptionType { get; set; }



        public SubscriptionType()
        {
            Invoices = new List<Invoice>();

            InvoiceSubscriptionType = new List<InvoiceSubscriptionType>();
        }
    }
}
