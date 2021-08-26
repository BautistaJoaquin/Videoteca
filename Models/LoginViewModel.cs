
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
 
namespace challenge_NET.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debes ingresar un Email")]
        public string Email { get; set; }
 
        [Required(ErrorMessage = "Debes ingresar una contraseña")]
        [StringLength(10, MinimumLength = 6)]
        public string Password { get; set; }
    }
}