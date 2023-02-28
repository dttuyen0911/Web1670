using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web1670.Models
{
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pubID { get; set; }
        public string pubName { get; set; }
        public string pubDescription { get; set; }
        public string pubAddress { get; set; }
        public int pubTelephone { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}
