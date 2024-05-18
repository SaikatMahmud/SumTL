﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.DAL.Models
{
    public class AppUser : IdentityUser
    {
        //[Required]
        //public string UserName { get; set; }
        public string? Address { get; set; }
    }
}
