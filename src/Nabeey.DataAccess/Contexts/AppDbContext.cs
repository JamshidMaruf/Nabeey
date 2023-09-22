using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Users;

namespace Nabeey.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    DbSet<User> Users { get; set; }
    DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Many to many connection between Users & Articles tables
        var userArticles = modelBuilder.Entity<UserArticle>();
        userArticles.HasKey(ua => new {ua.UserId, ua.ArticleId});
        userArticles.HasOne(ua => ua.User).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.UserId);
        userArticles.HasOne(ua => ua.Article).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.ArticleId);


    }
}