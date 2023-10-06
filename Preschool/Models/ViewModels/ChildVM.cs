using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Preschool.Models.ViewModels
{
    public class ChildVM
    {
        
        [Key]
        public int Id { get; set; }

        
       
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        
       
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        
       
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

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


        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }

        
       
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        public DateTime EnrolDate { get; set; }

        public int ClassroomId { get; set; }


        public bool IsActive { get; set; } = true;


        public List<string> DocumentCopies { get; set; }


        public int SubscriptionTypeId { get; set; }

        public ChildVM()
        {
            DocumentCopies = new List<string>();
        }

    }
}
