﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web1670.Models
{
    public class Category
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cateID { get; set; }
        [Required(ErrorMessage = "Name of category is not null")]
        [RegularExpression(@"^[a-zA-Z\s\W]+$", ErrorMessage = "Name must be character")]
        [StringLength(150, ErrorMessage = "String length no more than 150 characters")]
        public string cateName { get; set; }
        [Required(ErrorMessage = "Description of category is not null")]
        [StringLength(255, ErrorMessage = "String length no more than 255 characters")]
        public string cateDescription { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}
