using Kodmine.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kodmine.DAL.Repository
{
    public class BaseRepository<T> where T: class
    {
        protected readonly KodmineDbContext db;

        protected BaseRepository(KodmineDbContext db)
        {
            this.db = db;
        }

        public virtual void Create(T item)
        {
            db.Add(item);
            db.SaveChanges();
        }

        public virtual void Update(T item)
        {
            db.Update<T>(item);
            db.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var item = db.Find<T>(id);

            db.Remove(item);
            db.SaveChanges();
        }

        public virtual T GetById(int id)
        {
            return db.Find<T>(id);
        }

    }
}
