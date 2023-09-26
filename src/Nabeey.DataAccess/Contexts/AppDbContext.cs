using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities.QuestionAnswers;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.Contents;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Answers;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Users;
using Nabeey.Domain.Enums;
using Nabeey.Domain.Entities.QuizQuestions;

namespace Nabeey.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<ContentBook> ContentBooks { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<ContentAudio> ContentAudios { get; set; }
    public DbSet<ContentCategory> ContentCategories { get; set; }
    public DbSet<ContentVideo> ContentVideos { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<QuizQuestion> QuizQuestions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserArticle> UserArticles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Quiz>()
            .Property(e => e.StartTime)
            .HasColumnType("timestamp");
        modelBuilder.Entity<Quiz>()
            .Property(e => e.EndTime)
            .HasColumnType("timestamp");


        #region Entitylar uchun "IsDeleted" holatini filter qilish
        modelBuilder.Entity<Answer>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Article>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Asset>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Book>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<ContentBook>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Content>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<ContentAudio>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<ContentCategory>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<ContentVideo>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Question>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Quiz>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<QuizQuestion>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<UserArticle>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<QuestionAnswer>().HasQueryFilter(qa => !qa.IsDeleted);
        #endregion

        #region Many to many realationship --->>
        // Users <=> Articles
        //var userArticle = modelBuilder.Entity<UserArticle>();
        //userArticle.HasKey(ua => new { ua.UserId, ua.ArticleId });
        //userArticle.HasOne(ua => ua.User).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.UserId);
        //userArticle.HasOne(ua => ua.Article).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.ArticleId);

        // Quizzes <=> Questions
        modelBuilder.Entity<Quiz>()
            .HasOne(q => q.User)
            .WithMany(u => u.Quizzes)
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Quiz>()
            .HasOne(q => q.ContentCategory)
            .WithMany()
            .HasForeignKey(q => q.ContentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<QuizQuestion>()
            .HasKey(qq => new { qq.QuizId, qq.QuestionId });

        modelBuilder.Entity<QuizQuestion>()
            .HasOne(qq => qq.Quiz)
            .WithMany()
            .HasForeignKey(qq => qq.QuizId);

        modelBuilder.Entity<QuizQuestion>()
            .HasOne(qq => qq.Question)
            .WithMany()
            .HasForeignKey(qq => qq.QuestionId);

        // Questions <=> Answers
        var questionAnswer = modelBuilder.Entity<QuestionAnswer>();
        questionAnswer.HasKey(qa => new { qa.QuestionId, qa.AnswerId });
        #endregion
    }
}
