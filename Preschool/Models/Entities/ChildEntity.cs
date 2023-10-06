using Azure.Data.Tables;
using Azure;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;

namespace Preschool.Models.Entities
{
    public class ChildEntity : ITableEntity
    {
        public ChildEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public ChildEntity()
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
        [PersonalData]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } } // Computed property

        [Required]
        [PersonalData]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }


        [Required]
        [PersonalData]
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }

        [Required]
        public string Nationality { get; set; }

       

        [Required(ErrorMessage = "Father Telephone is required.")]
        [Display(Name = "Father Telephone")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Father Telephone must contain only numeric characters.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Father Telephone must be between 5 and 20 characters.")]
        public string FatherTelephone { get; set; }

        [Required(ErrorMessage = "Mother Telephone is required.")]
        [Display(Name = "Mother Telephone")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Mother Telephone must contain only numeric characters.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Mother Telephone must be between 5 and 20 characters.")]
        public string MotherTelephone { get; set; }

        [Required]
        [PersonalData]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirthUtc { get; set; }

        [Required]
        [Display(Name = "Enrol Date")]
        [DataType(DataType.Date)]
        public DateTime EnrolDateUtc { get; set; }


        [Required]
        public int ClassroomId { get; set; }




        // Define a property for storing the serialized JSON data
        public string SubscriptionsJson { get; set; }

      


        // Define a property for storing the serialized JSON data
        public string DocumentsImageJson { get; set; }

     


        // Define a property for storing the serialized JSON data
        public string AttendancesJson { get; set; }

        


        // Define a property for storing the serialized JSON data
        public string InvoicesJson { get; set; }

 
        
    }
}
