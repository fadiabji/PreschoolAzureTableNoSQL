using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace Preschool.Models.Entities
{
    public class SubscriptionEntity : ITableEntity
    {


        public SubscriptionEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public SubscriptionEntity()
        {
        }

        public string PartitionKey { get; set; } = default!;

        public string RowKey { get; set; } = default!;

        public DateTimeOffset? Timestamp { get; set; } = default!;

        public ETag ETag { get; set; } = default!;
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

        public string ChildJson { get; set; }

        public string SubscriptionTypeJson { get; set; }
    }
}
