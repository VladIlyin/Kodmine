using System.Collections.Generic;
using System.Linq;
using Kodmine.Core.Interfaces;
using Kodmine.DAL.Models;
using Kodmine.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Kodmine.DAL.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {

        public PostRepository(KodmineDbContext db) : base(db)
        {
        }

        public IEnumerable<Post> Get()
        {
            return db.Posts.Include(x => x.PostTags).ThenInclude(y => y.Tag);
        }

        public new Post GetById(int id)
        {
            //TODO: правильно реализовать запрос
            return Get().Where(i => i.PostId == id).FirstOrDefault();
        }

        public IEnumerable<Post> PostListMainPage(int take)
        {
            return db.Posts.Take(take).OrderByDescending(ord => ord.CreateDate).Include(x => x.PostTags).ThenInclude(y => y.Tag);
        }

        public void SaveContent(int id, string content)
        {
            var post = db.Posts.Find(id);
            if (post == null)
                return;

            post.Content = content;
            base.Update(post);
        }

    }
}
