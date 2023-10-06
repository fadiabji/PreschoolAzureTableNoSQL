using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class  User : IdentityUser
    {
            [PersonalData]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [PersonalData]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [PersonalData]
            [Display(Name = "Date Of Birth")]
            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; }


    }
}
