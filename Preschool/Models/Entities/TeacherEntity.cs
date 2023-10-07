using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace Preschool.Models.Entities
{
    public class TeacherEntity : ITableEntity
    {

        public TeacherEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public TeacherEntity()
        {
        }

        public string PartitionKey { get; set; } = default!;

        public string RowKey { get; set; } = default!;

        public DateTimeOffset? Timestamp { get; set; } = default!;

        public ETag ETag { get; set; } = default!;

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Teaching At")]
        public DateTime RegistedAt { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int ClassroomId { get; set; }

        public string ClassroomJson { get; set; }

        public string DocumentsImagesJson { get; set; }
    }
}
