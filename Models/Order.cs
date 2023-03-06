using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web1670.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderID { get; set; }
        public DateTime orderDate { get; set; }
        [Required(ErrorMessage = "Telephone of publisher is not null")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone numbers that only accept 10 numbers are allowed")]
        public string orderPhone { get; set; }
        [Required(ErrorMessage = "Address of publisher is not null")]
        [StringLength(255, ErrorMessage = "String length no more than 255 characters")]
        public string orderAddress { get; set; }
        [Required(ErrorMessage = "Name of category is not null")]
        [RegularExpression(@"^[a-zA-Z\s\W]+$", ErrorMessage = "Name must be character")]
        [StringLength(150, ErrorMessage = "String length no more than 150 characters")]
        public string orderFullname { get; set; }
        public double OrderTotal { get; set; }
        public string cus_id { get; set; }
        public string? owner_id { get; set; }
        public virtual ICollection<OrderDetail>? orderdetails { get; set; }
    }
}
