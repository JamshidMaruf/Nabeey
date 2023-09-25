using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities.Answers;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Contents;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.QuestionAnswers;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Entities.QuizQuestions;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Users;
using Nabeey.Domain.Enums;

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
    public DbSet<ContentImage> ContentImages { get; set; }
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

        #region Many to many realationship --->>
        // Users <=> Articles
        var userArticle = modelBuilder.Entity<UserArticle>();
        userArticle.HasKey(ua => new { ua.UserId, ua.ArticleId });
        userArticle.HasOne(ua => ua.User).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.UserId);
        userArticle.HasOne(ua => ua.Article).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.ArticleId);


        // Questions <=> Answers
        var questionAnswer = modelBuilder.Entity<QuestionAnswer>();
        questionAnswer.HasKey(qa => new { qa.QuestionId, qa.AnswerId });
        questionAnswer.HasOne(qa => qa.Answer).WithMany(qa => qa.QuestionAnswers).HasForeignKey(qa => qa.AnswerId);
        questionAnswer.HasOne(qa => qa.Question).WithMany(qa => qa.QuestionAnswers).HasForeignKey(qa => qa.QuestionId);
        #endregion

        modelBuilder.Entity<Content>()
            .HasOne<ContentImage>()
            .WithOne(ci => ci.Content)
            .IsRequired()
            .HasPrincipalKey<ContentImage>(ci => ci.Id)
            .HasForeignKey<Content>()
            .OnDelete(DeleteBehavior.Restrict);



        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "Imona",
                LastName = "Kabirova",
                Email = "imona.kabirova@example.com",
                Phone = "9001234567",
                PasswordHash = "a",
                UserRole = Role.User
            },
            new User
            {
                Id = 2,
                FirstName = "Jamshid",
                LastName = "Zayniev",
                Email = "jamshid.zayniev@example.com",
                Phone = "9007654321",
                PasswordHash = "a",
                UserRole = Role.User
            },
            new User
            {
                Id = 3,
                FirstName = "Anastasiya",
                LastName = "Tomchuk",
                Email = "anastasiya.tomchuk@example.com",
                Phone = "9009876543",
                PasswordHash = "a",
                UserRole = Role.User
            },
            new User
            {
                Id = 4,
                FirstName = "Iskandar",
                LastName = "Kodirov",
                Email = "iskandar.kodirov@example.com",
                Phone = "9012345678",
                PasswordHash = "a",
                UserRole = Role.User
            },
            new User
            {
                Id = 5,
                FirstName = "Nodirshax",
                LastName = "Allanazarov",
                Email = "nodirshax.allanazarov@example.com",
                Phone = "9012345679",
                PasswordHash = "a",
                UserRole = Role.User
            },
            new User
            {
                Id = 7, // Agar ma'lumotlar bazasida ishlatilmagan Id bo'lsa
                FirstName = "Asilbek",
                LastName = "Abdurashidov",
                Email = "asilbek.abdurashidov@example.com",
                Phone = "9012345679",
                PasswordHash = "a", // Iltimos, bezor password hash ishlatmang!
                UserRole = Role.User
            },
            new User
            {
                Id = 8, // Agar ma'lumotlar bazasida ishlatilmagan Id bo'lsa
                FirstName = "Jasurbek",
                LastName = "Ergashev",
                Email = "jasurbek.ergashev@example.com",
                Phone = "9012345680",
                PasswordHash = "a", // Iltimos, bezor password hash ishlatmang!
                UserRole = Role.User
            },
            new User
            {
                Id = 9, // Agar ma'lumotlar bazasida ishlatilmagan Id bo'lsa
                FirstName = "Takhmina",
                LastName = "Saidova",
                Email = "takhmina.saidova@example.com",
                Phone = "9012345681",
                PasswordHash = "a", // Iltimos, bezor password hash ishlatmang!
                UserRole = Role.User
            },
            new User
            {
                Id = 10, // Agar ma'lumotlar bazasida ishlatilmagan Id bo'lsa
                FirstName = "Asadbek",
                LastName = "Qarshiyev",
                Email = "asadbek.qarshiyev@example.com",
                Phone = "9012345682",
                PasswordHash = "a", // Iltimos, bezor password hash ishlatmang!
                UserRole = Role.User
            },
            new User
            {
                Id = 11, // Agar ma'lumotlar bazasida ishlatilmagan Id bo'lsa
                FirstName = "Sardor",
                LastName = "Sohinazarov",
                Email = "sardor.sohinazarov@example.com",
                Phone = "9012345683",
                PasswordHash = "a", // Iltimos, bezor password hash ishlatmang!
                UserRole = Role.User
            },
            new User
            {
                Id = 12, // Agar ma'lumotlar bazasida ishlatilmagan Id bo'lsa
                FirstName = "Raykhona",
                LastName = "Isroilova",
                Email = "raykhona.isroilova@example.com",
                Phone = "9012345684",
                PasswordHash = "a", // Iltimos, bezor password hash ishlatmang!
                UserRole = Role.User
            }
        );
    }
}
