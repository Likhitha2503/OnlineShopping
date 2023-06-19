﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopping_Web.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


    }
}
