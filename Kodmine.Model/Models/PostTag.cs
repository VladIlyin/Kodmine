using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Kodmine.Model.Models
{
    public class PostTag
    {
        public int PostTagId { get; set; }
        /// <summary>
        /// Номер поста
        /// </summary>
        //[ForeignKey("Post")]
        public int PostId { get; set; }

        /// <summary>
        /// Номер тэга
        /// </summary>
        //[ForeignKey("Tag")]
        public int TagId { get; set; }

        /// <summary>
        /// Пост
        /// </summary>
        public virtual Post Post { get; set; }

        /// <summary>
        /// Тэг
        /// </summary>
        public virtual Tag Tag { get; set; }

    }
}
