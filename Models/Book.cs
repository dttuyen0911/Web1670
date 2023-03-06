using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web1670.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bookID { get; set; }
        [Required(ErrorMessage = "Name of book is not null")]
        [RegularExpression(@"^[a-zA-Z\s\W]+$", ErrorMessage = "Name must be character")]
        [StringLength(150, ErrorMessage = "String length no more than 150 characters")]
        public string bookName { get; set; }
        [Required(ErrorMessage = "Description of book is not null")]
        public string bookDescription { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? urlImage { get; set; }
        public DateTime bookDate { get; set; }
        [RegularExpression(@"^\d[0-9]$", ErrorMessage = "Quantity is number")]
        public int? bookQuantity { get; set; }
        public int pubID  { get; set; }
        public int cateID { get; set; }
        [RegularExpression(@"^\d[0-9]$", ErrorMessage = "Quantity is number")]
        public double  bookPrice { get; set; }
        [ForeignKey("pubID")]
        public virtual Publisher? Publisher { get; set; }
        [ForeignKey("cateID")]
        public virtual Category? Category { get; set; }
    }
}
