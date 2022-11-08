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
        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string UserName { get; set; }
        [DisplayName("رمز")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Password { get; set; }
        [DisplayName("تکرار رمز")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "رمز با تکرار رمز مغایرت دارد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string RepeatPassword { get; set; }

    }
}
