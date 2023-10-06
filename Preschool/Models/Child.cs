using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;

namespace Preschool.Models
{
    public class Child
    {
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
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Enrol Date")]
        [DataType(DataType.Date)]
        public DateTime EnrolDate { get; set; }


        [Required]
        public int ClassroomId { get; set; }


        public virtual ICollection<Subscription> Subscriptions { get; set; }

        public virtual Classroom Classroom { get; set; }

        public virtual ICollection<DocumentsCopies> DocumentsImage { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

        public Child()
        {
            Subscriptions = new List<Subscription>();
        }
        

    }
}
