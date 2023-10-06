using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace Preschool.Models.Entities
{
    public class ClassroomEntity : ITableEntity
    {

        public ClassroomEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public ClassroomEntity()
        {
        }

        public string PartitionKey { get; set; } = default!;

        public string RowKey { get; set; } = default!;

        public DateTimeOffset? Timestamp { get; set; } = default!;

        public ETag ETag { get; set; } = default!;


        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Icon { get; set; }
        [Required]
        public string Color { get; set; }

        public string ChildrenJson { get; set; }
        public string TeachersJson { get; set; }
    }
}
