namespace Preschool.Models
{
    public class TeacherDocuemtnsCopy
    {
        public int Id { get; set; }
        public int DocumentsCopyId { get; set; }
        public virtual DocumentsCopies DocumentsCopy { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
