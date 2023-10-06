using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace Preschool.Models.Entities
{
    public class AssetEntity : ITableEntity
    {

        public AssetEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public AssetEntity()
        {
        }

        public string PartitionKey { get; set; } = default!;

        public string RowKey { get; set; } = default!;

        public DateTimeOffset? Timestamp { get; set; } = default!;

        public ETag ETag { get; set; } = default!;

        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Caption { get; set; }

        [Display(Name = "Registered At")]
        [DataType(DataType.Date)]
        public DateTime RegisterdAtUtc { get; set; }
    }
}
