using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
    
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "Expire At")]
        public DateTime ExpireAt { get; set; } 

        [Required]
        public bool PaymentComplete { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public int ChildId { get; set; }
     
        public int SubscriptionTypeId { get; set; }
     
        public virtual Child Child { get; set; }
     
        public virtual SubscriptionType SubscriptionType { get; set; }
    }
}
