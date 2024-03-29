﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web1670.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cartID { get; set; }
        public int cartQuantity { set; get; }
        public string cus_id { get; set; }
        public Book book { set; get; }
    }
}
