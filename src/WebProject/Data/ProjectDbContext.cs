using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProject.Models.Account;
using WebProject.Areas.Library.Models;
using WebProject.Areas.Account.Models;

namespace WebProject.Data
{
    public class ProjectDbContext : IdentityDbContext<User>
    {
        public DbSet<Library> LibraryProjects { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticlePart> ArticleParts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }

        public ProjectDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ArticleCategory>().HasKey(key => new { key.ArticleId, key.CategoryId }); 
            modelBuilder.Entity<ArticleCategory>()
                .HasOne(ac => ac.Article)
                .WithMany(ac => ac.ArticleCategories)
                .HasForeignKey(at => at.ArticleId);
            modelBuilder.Entity<ArticleCategory>()
                .HasOne(ac => ac.Category)
                .WithMany(ac => ac.ArticleCategories)
                .HasForeignKey(ac => ac.CategoryId);

            modelBuilder.Entity<ArticleTag>().HasKey(key => new { key.ArticleId, key.TagId });
            modelBuilder.Entity<ArticleTag>()
                .HasOne(at => at.Tag)
                .WithMany(at => at.ArticleTags)
                .HasForeignKey(at => at.TagId);
            modelBuilder.Entity<ArticleTag>()
                .HasOne(at => at.Article)
                .WithMany(at => at.ArticleTags)
                .HasForeignKey(at => at.ArticleId);
        }
    }
}
