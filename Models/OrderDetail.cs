using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web1670.Models
{
    public class OrderDetail
    {
        [Key]
        [Column(Order = 1)]
        public int orderID { get; set; }
        [ForeignKey("orderID")]
        public virtual Order? Order { get; set; }
        [Key]
        [Column(Order = 2)]
        public int bookID { get; set; }

        [ForeignKey("bookID")]
        public virtual Book? Book { get; set; }

        [ForeignKey("orderID")]
        public double price { get; set; } 
        public int quantity { get; set; } //quantity of book
        public double amount { get; set; } //total quantity of book
        public DateTime OrderDetailDate { get; set; }

    }
}
