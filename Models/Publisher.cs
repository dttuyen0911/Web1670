using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web1670.Models
{
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pubID { get; set; }
        [Required(ErrorMessage = "Name of publisher is not null")]
        [RegularExpression(@"^[a-zA-Z\s\W]+$", ErrorMessage = "Name must be character")]
        [StringLength(150, ErrorMessage = "String length no more than 150 characters")]
        public string pubName { get; set; }
        [Required(ErrorMessage = "Description of publisher is not null")]
        [StringLength(255, ErrorMessage = "String length no more than 255 characters")]
        public string pubDescription { get; set; }
        [Required(ErrorMessage = "Address of publisher is not null")]
        [StringLength(255, ErrorMessage = "String length no more than 255 characters")]
        public string pubAddress { get; set; }
        [Required(ErrorMessage = "Telephone of publisher is not null")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone numbers that only accept 10 numbers are allowed")]
        public int pubTelephone { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}
