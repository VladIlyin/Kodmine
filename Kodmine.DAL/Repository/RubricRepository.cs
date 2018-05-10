using Kodmine.Core.Interfaces;
using Kodmine.DAL.Models;
using Kodmine.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kodmine.DAL.Repository
{
    public class RubricRepository : BaseRepository<Rubric>, IRubricRepository
    {
        public RubricRepository(KodmineDbContext db) : base(db)
        {
        }

        public IEnumerable<Rubric> Get()
        {
            return db.Rubrics.ToList();
        }

    }
}
