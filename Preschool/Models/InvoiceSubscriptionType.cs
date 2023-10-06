using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class InvoiceSubscriptionType
    {


        [Required]
        public int Id { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public int SubscriptionTypeId { get; set; }


        public virtual SubscriptionType SubscriptionType { get; set;}

        public virtual Invoice Invoice { get; set;}
    }
}
