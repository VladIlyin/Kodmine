using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kodmine.Model.Models
{
    public class Rubric
    {
        [Key]
        public int RubricId { get; set; }
        public string Name { get; set; }
        public virtual Post Post { get; set; }
    }
}
