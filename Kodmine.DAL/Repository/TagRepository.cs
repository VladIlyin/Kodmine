using System.Collections.Generic;
using System.Linq;
using Kodmine.Core.Interfaces;
using Kodmine.DAL.Models;
using Kodmine.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Kodmine.DAL.Repository
{
    public class TagRepository : ITagRepository
    {

        KodmineDbContext db;

        public TagRepository(KodmineDbContext db)
        {
            this.db = db;
        }

        public void Create(Kodmine.Model.Models.Tag item)
        {
            db.Tags.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var tag = db.Tags.AsNoTracking().SingleOrDefault(m => m.TagId == id);

            db.Remove(tag);
            db.SaveChanges();
        }

        public IEnumerable<Tag> Get()
        {
            //using (KodmineDbContext db = new KodmineDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<KodmineDbContext>()))
            //{
            var data = db.Tags.Include(x => x.PostTags).ToList();
            return data;
            //}
        }

        public Tag GetById(int id)
        {
            return db.Tags.Find(id);
        }

        public void Update(Tag item)
        {
            db.Tags.Attach(item);
            db.Entry(item).State = EntityState.Modified;

            db.SaveChanges();
        }
    }
}