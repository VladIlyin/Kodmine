using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kodmine.Areas.Admin.Models
{
    public class Role
    {
        [Required]
        [Display(Name = "Имя роли")]
        public string Name { get; set; }
    }
}
