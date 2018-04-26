using Kodmine.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kodmine.DAL.Repository
{
    public class BaseRepository
    {
        protected readonly KodmineDbContext db;

        protected BaseRepository(KodmineDbContext db)
        {
            this.db = db;
        }
    }
}
