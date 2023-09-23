using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities;
using Nabeey.Domain.Entities.Answers;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.ContentBooks;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.QuestionAnswers;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Users;

namespace Nabeey.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<ContentBook> ContentBooks { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<ContentAudio> ContentAudios { get; set; }
    public DbSet<ContentCategory> ContentCategories { get; set; }
    public DbSet<ContentImage> ContentImages { get; set; }
    public DbSet<ContentVideo> ContentVideos { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<QuizQuestion> QuizQuestions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserArticle> UserArticles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // entitilar uchun "IsDeleted" holatini filter qilish
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

        #region Many to many realationship --->>
        // Users <=> Articles
        var userArticle = modelBuilder.Entity<UserArticle>();
        userArticle.HasKey(ua => new { ua.UserId, ua.ArticleId });
        userArticle.HasOne(ua => ua.User).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.UserId);
        userArticle.HasOne(ua => ua.Article).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.ArticleId);

        // Quizzes <=> Questions
        var quizQuestion = modelBuilder.Entity<QuizQuestion>();
        quizQuestion.HasKey(qq => new { qq.QuizId, qq.QuestionId });
        quizQuestion.HasOne(qq => qq.Quiz).WithMany(qq => qq.QuizQuestions).HasForeignKey(qq => qq.QuizId);
        quizQuestion.HasOne(qq => qq.Question).WithMany(qq => qq.QuizQuestions).HasForeignKey(qq => qq.QuestionId);

        // Questions <=> Answers
        var questionAnswer = modelBuilder.Entity<QuestionAnswer>();
        questionAnswer.HasKey(qa => new { qa.QuestionId, qa.AnswerId });
        questionAnswer.HasOne(qa => qa.Answer).WithMany(qa => qa.QuestionAnswers).HasForeignKey(qa => qa.AnswerId);
        questionAnswer.HasOne(qa => qa.Question).WithMany(qa => qa.QuestionAnswers).HasForeignKey(qa => qa.QuestionId);
        #endregion

        // entity uchun global query filter ni o'chirish
        modelBuilder.Entity<Asset>().HasQueryFilter(null);
        modelBuilder.Entity<Answer>().HasQueryFilter(null);
        modelBuilder.Entity<Article>().HasQueryFilter(null);
        modelBuilder.Entity<User>().HasQueryFilter(null);
        modelBuilder.Entity<Content>().HasQueryFilter(null);
        modelBuilder.Entity<Question>().HasQueryFilter(null);
        modelBuilder.Entity<ContentCategory>().HasQueryFilter(null);
    }
}