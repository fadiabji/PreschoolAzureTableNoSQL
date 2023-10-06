namespace Preschool.Models
{
    public class ChildDocumentsCopy
    {
        public int Id { get; set; }
        public int DocumentsCopyId { get; set; }
        public virtual DocumentsCopies DocumentsCopy { get; set; }
        public int ChildId { get; set; }
        public virtual Child Child { get; set; }
    }
}
