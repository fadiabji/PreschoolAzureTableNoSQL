using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Preschool.Models.ViewModels
{
    public class TeacherVM
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [PersonalData]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [PersonalData]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [PersonalData]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RegistedAt { get; set; }

        public bool IsActive { get; set; }

        public int ClassroomId { get; set; }

        public List<string> DocumentCopies { get; set; }

      
        public TeacherVM()
        {
            DocumentCopies = new List<string>();
        }
    }
}
