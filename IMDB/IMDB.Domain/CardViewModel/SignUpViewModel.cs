using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Domain.CardViewModel
{
	public class SignUpViewModel
	{
        [DisplayName("Username")]
        [Required(ErrorMessage = "Please enter Enter the {0} ")]
        public string UserName { get; set; }
        [DisplayName("password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter Enter the {0} ")]
        public string Password { get; set; }
        [DisplayName("repeating of password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "password not equlse repeating password")]
        [Required(ErrorMessage = "enter Enter the {0}")]
        public string RepeatPassword { get; set; }

    }
}
