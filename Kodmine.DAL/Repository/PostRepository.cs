using System.Collections.Generic;
using System.Linq;
using Kodmine.Core.Interfaces;
using Kodmine.DAL.Models;
using Kodmine.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Kodmine.DAL.Repository
{
    public class PostRepository : BaseRepository, IPostRepository
    {

        public PostRepository(KodmineDbContext db) : base(db)
        {
        }

        public void Create(Post item)
        {
            db.Posts.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var post = db.Posts.AsNoTracking().SingleOrDefault(m => m.PostId == id);

            db.Remove(post);
            db.SaveChanges();
        }

        public IEnumerable<Post> Get()
        {
            return db.Posts.Include(x => x.PostTags).ThenInclude(y => y.Tag).ToList();
        }

        public Post GetById(int id)
        {
            return db.Posts.Include(x => x.PostTags).ThenInclude(y => y.Tag).Where(i => i.PostId == id).FirstOrDefault();
        }

        public void SaveContent(int id, string content)
        {
            var post = db.Posts.Find(id);
            if (post == null)
                return;

            post.Content = content;
            Update(post);
        }

        public void Update(Post item)
        {
            db.Posts.Attach(item);
            db.Entry(item).State = EntityState.Modified;

            db.SaveChanges();
        }
    }
}
