using Kodmine.Core.Interfaces;
using Kodmine.DAL.Models;
using Kodmine.Model.Models;
using System.Linq;

namespace Kodmine.DAL.Repository
{
    public class PostTagRepository : BaseRepository<PostTag>, IPostTagRepository
    {
        public PostTagRepository(KodmineDbContext db) : base(db)
        {
        }

        public void AddTagToPost(int tagId, int postId)
        {
            base.Create(new PostTag() { TagId = tagId, PostId = postId });
        }

        public void RemoveTagFromPost(int tagId, int postId)
        {
            var postTag = db.PostTags.Where(x => x.TagId == tagId && x.PostId == postId).FirstOrDefault();
            base.Delete(postTag.PostTagId);
        }
    }
}
