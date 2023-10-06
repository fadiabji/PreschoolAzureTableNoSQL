using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Attendance
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ChildId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public bool Status { get; set; } = false;  // true for present, false for absent

        public virtual Child Child { get; set; }
    }

}
