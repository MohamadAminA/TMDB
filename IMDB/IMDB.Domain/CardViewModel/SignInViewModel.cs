using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Domain.CardViewModel
{
    public class SignInViewModel
    {
        [DisplayName("User name")]
        [Required(ErrorMessage = "Please enter the {0}")]
        public string UserName { get; set; }
        [DisplayName("password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter the {0}")]
        public string Password { get; set; }
        [DisplayName("remember me")]
        public bool RememberMe { get; set; }
    }
}
