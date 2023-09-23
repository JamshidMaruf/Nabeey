using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities;
using Nabeey.Domain.Entities.Answers;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Users;

namespace Nabeey.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    DbSet<Answer> Answers { get; set; }
    DbSet<Article> Articles { get; set; }
    DbSet<Question> Questions { get; set; }
    DbSet<Quiz> Quizzes { get; set; }
    DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Many to many realationship --->>
        // Users <=> Articles
        var userArticle = modelBuilder.Entity<UserArticle>();
        userArticle.HasKey(ua => new {ua.UserId, ua.ArticleId});
        userArticle.HasOne(ua => ua.User).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.UserId);
        userArticle.HasOne(ua => ua.Article).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.ArticleId);
        
        // Quizzes <=> Questions
        var quizQuestion = modelBuilder.Entity<QuizQuestion>();
        quizQuestion.HasKey(qq => new { qq.QuizId, qq.QuestionId });
        quizQuestion.HasOne(qq => qq.Quiz).WithMany(qq => qq.QuizQuestions).HasForeignKey(qq => qq.Quiz);
        quizQuestion.HasOne(qq => qq.Question).WithMany(qq => qq.QuizQuestions).HasForeignKey(qq => qq.QuestionId);
        
        // Questions <=> Answers
        var questionAnswer = modelBuilder.Entity<QuestionAnswer>();
        questionAnswer.HasKey(qa => new { qa.QuestionId, qa.AnswerId });
        questionAnswer.HasOne(qa => qa.Answer).WithMany(qa => qa.QuestionAnswers).HasForeignKey(qa => qa.AnswerId);
        questionAnswer.HasOne(qa => qa.Question).WithMany(qa => qa.QuestionAnswers).HasForeignKey(qa => qa.QuestionId);
        #endregion

    }
}