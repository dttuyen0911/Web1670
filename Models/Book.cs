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
        //public IFormFile bookUpload { get; set; }
        public double bookPrice { get; set; }
        public int pubID { get; set; }
        [ForeignKey("pubID")] //take cate_id is foreignKey
        //public int cart_id { get; set; }
        //[ForeignKey("cart_id")]
        public virtual Publisher? Publisher { get; set; }
    }
}
