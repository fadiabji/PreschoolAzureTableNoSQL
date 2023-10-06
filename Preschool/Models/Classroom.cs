using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Classroom
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Icon { get; set; }
        [Required]
        public string Color { get; set; }

        public virtual ICollection<Child> Children { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }

        //public Classroom()
        //{
        //    var ListOfStudents = new List<Child>();
        //}

    }
}
