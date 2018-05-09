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
    public class RubricRepository : BaseRepository, IRubricRepository
    {
        public RubricRepository(KodmineDbContext db) : base(db)
        {
        }

        public void Create(Rubric item)
        {
            db.Rubrics.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var rubric = db.Rubrics.Find(id);

            db.Remove(rubric);
            db.SaveChanges();
        }

        public IEnumerable<Rubric> Get()
        {
            return db.Rubrics.ToList();
        }

        public Rubric GetById(int id)
        {
            return db.Rubrics.Find(id);
        }

        public void Update(Rubric item)
        {
            db.Rubrics.Attach(item);
            db.Entry(item).State = EntityState.Modified;

            db.SaveChanges();
        }
    }
}
