using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Teacher
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        public string FullName { get { return FirstName + " " + LastName; } } // Computed property


        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Teaching At")]
        public DateTime RegistedAt { get; set; }

        public bool  IsActive { get; set; }

        [Required]
        public int ClassroomId { get; set; }

        public virtual Classroom Classroom { get; set; }

        public virtual ICollection<DocumentsCopies> DocumentsImage { get; set; }



    }
}
