using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace example.API.Models.User
{
    public class RegisterViewModelInput
    {
        [Required(ErrorMessage ="Login obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Email obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password obrigatório")]
        public string Password { get; set; }
    }
}
