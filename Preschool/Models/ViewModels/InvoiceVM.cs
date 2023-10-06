using System.ComponentModel.DataAnnotations;

namespace Preschool.Models.ViewModels
{
    public class InvoiceVM
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Invoice Number field is required.")]
        public string InvoiceNumber { get; set; } = new Random().Next(1000000, 9999999).ToString();


        [DataType(DataType.Date)]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        // Optional discount field
        [Display(Name = "Discount")]
        [Range(0, 1, ErrorMessage = "Discount must be between 0 and 1.")]
        public decimal? Discount { get; set; }

       
        public List<string> InvoiceItems { get; set; }

        
        public decimal Payment { get; set; }

        [Required]
        public int ChildId { get; set; }
        

        public string LogoFileAddress { get; set; }
        public string QRCodeFileAddress { get; set; }
        
        
        public InvoiceVM()
        {
            InvoiceItems = new List<string>();
        }

    }
}
