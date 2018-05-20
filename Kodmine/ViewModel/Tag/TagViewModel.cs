using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kodmine.Model.Models
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
