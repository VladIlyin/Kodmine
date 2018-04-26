using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kodmine.Model.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
