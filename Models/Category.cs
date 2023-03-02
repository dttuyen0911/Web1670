using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web1670.Models
{
    public class Category
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cateID { get; set; }
        public string cateName { get; set; }
        public string cateDescription { get; set; }
        public string cateAddress { get; set; }
        public int cateTelephone { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}
