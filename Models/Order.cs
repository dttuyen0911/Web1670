using System.ComponentModel.DataAnnotations;

namespace Web1670.Models
{
    public class Order
    {
        [Key]
        public int orderID { get; set; }
        //public string cus_name { get; set; }
        public virtual ICollection<OrderDetail> orderdetails { get; set; }
    }
}
