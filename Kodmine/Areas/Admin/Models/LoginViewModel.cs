using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kodmine.Areas.Admin.Models
{
    public class LoginViewModel
    {
        [DisplayName("Логин")]
        public string Email { get; set; }
        [DisplayName("Пароль")]
        public string Password  { get; set; }
        [DisplayName("Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}
