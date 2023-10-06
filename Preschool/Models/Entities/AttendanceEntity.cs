using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace Preschool.Models.Entities
{
    public class AttendanceEntity : ITableEntity
    {

        public AttendanceEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public AttendanceEntity()
        {
        }

        public string PartitionKey { get; set; } = default!;

        public string RowKey { get; set; } = default!;

        public DateTimeOffset? Timestamp { get; set; } = default!;

        public ETag ETag { get; set; } = default!;

        [Required]
        public int Id { get; set; }
        [Required]
        public int ChildId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public bool Status { get; set; } = false;  // true for present, false for absent

        public string Child { get; set; }
    }
}
