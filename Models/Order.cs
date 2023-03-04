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
        public string orderPhone { get; set; }
        public string orderAddress { get; set; }
        public string orderFullname { get; set; }
        //public string cus_name { get; set; }
        public double OrderTotal { get; set; }
        public string cus_id { get; set; }
        public string? owner_id { get; set; }
        public virtual ICollection<OrderDetail>? orderdetails { get; set; }
    }
}
