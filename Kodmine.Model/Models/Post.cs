using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Kodmine.Model.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [DisplayName("Заголовок")]
        public string Title { get; set; }
        [DisplayName("Контент")]
        public string Content { get; set; }
        [DisplayName("Дата создания")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreateDate { get; set; }

        [NotMapped]
        public string ContentShortened
        {
            get
            {
                if (Content?.Length > 200)
                    return Content.Substring(0, 200);
                else
                    return Content;
            }
        }

        //[ForeignKey("Rubric")]
        public int RubricId { get; set; }

        public virtual Rubric Rubric { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
