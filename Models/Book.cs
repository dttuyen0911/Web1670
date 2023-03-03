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
        [Required]
        public int bookID { get; set; }
        public string bookName { get; set; }
        public string bookDescription { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? urlImage { get; set; }
        public DateTime bookDate { get; set; }
        public int bookQuantity { get; set; }
        public int pubID  { get; set; }
        public int cateID { get; set; }
        public decimal bookPrice { get; set; }
        [ForeignKey("pubID")]
        public virtual Publisher? Publisher { get; set; }
        [ForeignKey("cateID")]
        public virtual Category? Category { get; set; }
    }
}
