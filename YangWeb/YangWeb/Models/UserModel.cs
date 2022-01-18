﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YangWeb.Models
{
    public class UserModel
    {

        public int ID { get; set; }

        [StringLength(10, MinimumLength = 5)]
        [DataType(DataType.Text)]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
