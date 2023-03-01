﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class Book
    {
        [Key]
        [Required]
        public int BookID { get; set; }
        [Required]

        public string Title { get; set; }
        [Required]

        public string Author { get; set; }
        [Required]

        public string Publisher { get; set; }
        [Required]

        public int ISBN { get; set; }
        [Required]
        public string Classification { get; set; }
        [Required]

        public string Category { get; set; }
        [Required]


        public int PageCount { get; set; }
        [Required]

        public double Price { get; set; }

    } // ? means the field is nullable
}
