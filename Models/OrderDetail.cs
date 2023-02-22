using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web1670.Models
{
    public class OrderDetail
    {
        [Key]
        [Column(Order = 1)]
        public int orderID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int bookID { get; set; }
        [ForeignKey("orderID")]
        public virtual Order order { get; set; }
        [ForeignKey("bookID")]
        public virtual Book book { get; set; }
    }
}
