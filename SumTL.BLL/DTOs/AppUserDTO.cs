using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.DTOs
{
    public class AppUserDTO : IdentityUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Password")]
        public string PasswordHash { get; set; }
        public string? Address { get; set; }
    }
}
