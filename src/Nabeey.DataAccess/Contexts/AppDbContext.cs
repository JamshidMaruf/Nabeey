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
        modelBuilder.Entity<UserArticle>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<QuestionAnswer>().HasQueryFilter(qa => !qa.IsDeleted);

        #region Many to many realationship --->>
        // Users <=> Articles
        //var userArticle = modelBuilder.Entity<UserArticle>();
        //userArticle.HasKey(ua => new { ua.UserId, ua.ArticleId });
        //userArticle.HasOne(ua => ua.User).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.UserId);
        //userArticle.HasOne(ua => ua.Article).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.ArticleId);

        // Quizzes <=> Questions
        var quizQuestion = modelBuilder.Entity<QuizQuestion>();
        quizQuestion.HasKey(qq => new { qq.QuizId, qq.QuestionId });

        // Questions <=> Answers
        var questionAnswer = modelBuilder.Entity<QuestionAnswer>();
        questionAnswer.HasKey(qa => new { qa.QuestionId, qa.AnswerId });
        questionAnswer.HasOne(qa => qa.Answer).WithMany(qa => qa.QuestionAnswers).HasForeignKey(qa => qa.AnswerId);
        questionAnswer.HasOne(qa => qa.Question).WithMany(qa => qa.QuestionAnswers).HasForeignKey(qa => qa.QuestionId);
        #endregion

        //modelBuilder.Entity<Content>()
        //    .HasOne<ContentImage>()
        //    .WithOne(ci => ci.Content)
        //    .IsRequired()
        //    .HasPrincipalKey<ContentImage>(ci => ci.Id)
        //    .HasForeignKey<Content>()
        //    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 3, Title = "1984", Author = "George Orwell", Description = "Dystopian novel", Text = "Text 3" },
            new Book { Id = 4, Title = "To Kill a Mockingbird", Author = "Harper Lee", Description = "Classic novel", Text = "Text 4" },
            new Book { Id = 5, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Description = "American classic", Text = "Text 5" },
            new Book { Id = 6, Title = "Pride and Prejudice", Author = "Jane Austen", Description = "Romantic novel", Text = "Text 6" },
            new Book { Id = 7, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Description = "Coming-of-age novel", Text = "Text 7" },
            new Book { Id = 8, Title = "Lord of the Rings", Author = "J.R.R. Tolkien", Description = "Epic fantasy", Text = "Text 8" },
            new Book { Id = 9, Title = "Harry Potter and the Sorcerer's Stone", Author = "J.K. Rowling", Description = "Fantasy novel", Text = "Text 9" },
            new Book { Id = 10, Title = "The Hobbit", Author = "J.R.R. Tolkien", Description = "Fantasy adventure", Text = "Text 10" },
            new Book { Id = 11, Title = "Oliver Twist", Author = "Charles Dickens", Description = "Roman klassikasi", Text = "Matni 11" },
            new Book { Id = 12, Title = "Sherlok Holms", Author = "Arthyr Konan Doil", Description = "Mashhur detektiv qissalari", Text = "Matni 12" },
            new Book { Id = 13, Title = "Qo'shiqchi", Author = "Fyodor Dostoyevski", Description = "Rus roman klassikasi", Text = "Matni 13" },
            new Book { Id = 14, Title = "Zulumotlar va qo'rqinlar vaqti", Author = "H. P. Lovecraft", Description = "Fantastika", Text = "Matni 14" },
            new Book { Id = 15, Title = "Qorquv", Author = "Stephen King", Description = "G'azablandiruvchi roman", Text = "Matni 15" },
            new Book { Id = 16, Title = "Mobi-Dik", Author = "Herman Melville", Description = "Qayiq ko'prik qirg'ishi", Text = "Matni 16" },
            new Book { Id = 17, Title = "Andijonlik", Author = "Munis Xo'ja", Description = "Xalq qahramoni tarixiy roman", Text = "Matni 17" },
            new Book { Id = 18, Title = "Sulton Kuzo", Author = "Alexander Duma", Description = "Maktab roman klassikasi", Text = "Matni 18" },
            new Book { Id = 19, Title = "Qo'rqinchli g'azal", Author = "Edgar Allan Poe", Description = "G'azallar", Text = "Matni 19" },
            new Book { Id = 20, Title = "Bir kunda", Author = "Ivan Denisovich", Description = "Olam shekillari roman", Text = "Matni 20" }
        );


        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, FirstName = "Imona", LastName = "Kabirova", Email = "imona.kabirova@example.com", Phone = "9001234567", PasswordHash = "a", UserRole = Role.User },
            new User { Id = 2, FirstName = "Jamshid", LastName = "Zayniev", Email = "jamshid.zayniev@example.com", Phone = "9007654321", PasswordHash = "a", UserRole = Role.User },
            new User { Id = 3, FirstName = "Anastasiya", LastName = "Tomchuk", Email = "anastasiya.tomchuk@example.com", Phone = "9009876543", PasswordHash = "a", UserRole = Role.User },
            new User { Id = 4, FirstName = "Iskandar", LastName = "Kodirov", Email = "iskandar.kodirov@example.com", Phone = "9012345678", PasswordHash = "a", UserRole = Role.User },
            new User { Id = 5, FirstName = "Nodirshax", LastName = "Allanazarov", Email = "nodirshax.allanazarov@example.com", Phone = "9012345679", PasswordHash = "a", UserRole = Role.User },
            new User { Id = 7, FirstName = "Asilbek", LastName = "Abdurashidov", Email = "asilbek.abdurashidov@example.com", Phone = "9012345679", PasswordHash = "a", UserRole = Role.User },
            new User { Id = 8, FirstName = "Jasurbek", LastName = "Ergashev", Email = "jasurbek.ergashev@example.com", Phone = "9012345680", PasswordHash = "a", UserRole = Role.User },
            new User { Id = 9, FirstName = "Takhmina", LastName = "Saidova", Email = "takhmina.saidova@example.com", Phone = "9012345681", PasswordHash = "a", UserRole = Role.User },
            new User { Id = 10, FirstName = "Asadbek", LastName = "Qarshiyev", Email = "asadbek.qarshiyev@example.com", Phone = "9012345682", PasswordHash = "a", UserRole = Role.User },
            new User { Id = 11, FirstName = "Sardor", LastName = "Sohinazarov", Email = "sardor.sohinazarov@example.com", Phone = "9012345683", PasswordHash = "a", UserRole = Role.User },
            new User { Id = 12, FirstName = "Raykhona", LastName = "Isroilova", Email = "raykhona.isroilova@example.com", Phone = "9012345684", PasswordHash = "a", UserRole = Role.User }
        );
    }
}
