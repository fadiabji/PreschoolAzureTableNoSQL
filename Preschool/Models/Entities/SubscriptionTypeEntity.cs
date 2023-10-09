using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace Preschool.Models.Entities
{
    public class SubscriptionTypeEntity : ITableEntity
    {
        public SubscriptionTypeEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public SubscriptionTypeEntity(){}

        public string PartitionKey { get; set; } = default!;

        public string RowKey { get; set; } = default!;

        public DateTimeOffset? Timestamp { get; set; } = default!;

        public ETag ETag { get; set; } = default!;
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Duration Month")]
        public int DurationMonth { get; set; }

        [Required]
        public double Price { get; set; }

        public string InvoicesJson { get; set; }
        public string InvoiceSubscriptionTypeJson { get; set; }
        
    }
}
