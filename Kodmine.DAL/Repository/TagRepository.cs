using System.Collections.Generic;
using System.Linq;
using Kodmine.Core.Interfaces;
using Kodmine.DAL.Models;
using Kodmine.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Kodmine.DAL.Repository
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {

        public TagRepository(KodmineDbContext db) : base(db)
        {
        }

        public IEnumerable<Tag> Get()
        {
            var data = db.Tags.Include(x => x.PostTags).ToList();
            return data;
        }

    }
}