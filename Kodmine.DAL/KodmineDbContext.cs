using Kodmine.Model.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kodmine.DAL.Models
{
    public class KodmineDbContext : IdentityDbContext<ApplicationUser>
    {
        public KodmineDbContext(DbContextOptions<KodmineDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Rubric)
                .WithMany(r => r.Posts)
                .HasForeignKey(p => p.RubricId)
                .HasConstraintName("FK_Post_Rubric")
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Kodmine.Model.Models.Post> Posts { get; set; }
        public DbSet<Kodmine.Model.Models.Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Kodmine.Model.Models.Rubric> Rubrics { get; set; }

    }
}
