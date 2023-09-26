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
        questionAnswer.HasOne(qa => qa.Answer).WithMany(qa => qa.QuestionAnswers).HasForeignKey(qa => qa.AnswerId);
        questionAnswer.HasOne(qa => qa.Question).WithMany(qa => qa.QuestionAnswers).HasForeignKey(qa => qa.QuestionId);
        #endregion

        #region SEED DATA
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 3, Title = "1984", Author = "George Orwell", Description = "Dystopian novel"},
            new Book { Id = 4, Title = "To Kill a Mockingbird", Author = "Harper Lee", Description = "Classic novel"},
            new Book { Id = 5, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Description = "American classic" },
            new Book { Id = 6, Title = "Pride and Prejudice", Author = "Jane Austen", Description = "Romantic novel" },
            new Book { Id = 7, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Description = "Coming-of-age novel" },
            new Book { Id = 8, Title = "Lord of the Rings", Author = "J.R.R. Tolkien", Description = "Epic fantasy" },
            new Book { Id = 9, Title = "Harry Potter and the Sorcerer's Stone", Author = "J.K. Rowling", Description = "Fantasy novel" },
            new Book { Id = 10, Title = "The Hobbit", Author = "J.R.R. Tolkien", Description = "Fantasy adventure" },
            new Book { Id = 11, Title = "Oliver Twist", Author = "Charles Dickens", Description = "Roman klassikasi" },
            new Book { Id = 12, Title = "Sherlok Holms", Author = "Arthyr Konan Doil", Description = "Mashhur detektiv qissalari" },
            new Book { Id = 13, Title = "Qo'shiqchi", Author = "Fyodor Dostoyevski", Description = "Rus roman klassikasi" },
            new Book { Id = 14, Title = "Zulumotlar va qo'rqinlar vaqti", Author = "H. P. Lovecraft", Description = "Fantastika" },
            new Book { Id = 15, Title = "Qorquv", Author = "Stephen King", Description = "G'azablandiruvchi roman" },
            new Book { Id = 16, Title = "Mobi-Dik", Author = "Herman Melville", Description = "Qayiq ko'prik qirg'ishi" },
            new Book { Id = 17, Title = "Andijonlik", Author = "Munis Xo'ja", Description = "Xalq qahramoni tarixiy roman" },
            new Book { Id = 18, Title = "Sulton Kuzo", Author = "Alexander Duma", Description = "Maktab roman klassikasi" },
            new Book { Id = 19, Title = "Qo'rqinchli g'azal", Author = "Edgar Allan Poe", Description = "G'azallar" },
            new Book { Id = 20, Title = "Bir kunda", Author = "Ivan Denisovich", Description = "Olam shekillari roman" }
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
        modelBuilder.Entity<ContentCategory>().HasData(
            new ContentCategory { Id = 1, Name = "Darsliklar" },
            new ContentCategory { Id = 2, Name = "Romanlar" },
            new ContentCategory { Id = 3, Name = "Maqolalar" },
            new ContentCategory { Id = 4, Name = "She'rlar" },
            new ContentCategory { Id = 5, Name = "Tadbirlar" }
        );
        modelBuilder.Entity<Quiz>().HasData(
            new Quiz { Id = 1, Name = "Matematika Testi", Description = "Matematika mavzusida umumiy test", QuestionCount = 20, StartTime = new DateTime(2023, 9, 5, 9, 0, 0), EndTime = new DateTime(2023, 9, 5, 10, 30, 0), UserId = 1, ContentCategoryId = 1 },
            new Quiz { Id = 2, Name = "Ingliz Tilini Testlash", Description = "Ingliz tili bilimlarini sinovlash testi", QuestionCount = 15, StartTime = new DateTime(2023, 9, 6, 14, 0, 0), EndTime = new DateTime(2023, 9, 6, 15, 30, 0), UserId = 2, ContentCategoryId = 1 },
            new Quiz { Id = 3, Name = "Fizika Fan Testi", Description = "Fizika fanidan malakalaringizni sinovlash testi", QuestionCount = 25, StartTime = new DateTime(2023, 9, 7, 10, 0, 0), EndTime = new DateTime(2023, 9, 7, 11, 30, 0), UserId = 3, ContentCategoryId = 1 },
            new Quiz { Id = 4, Name = "Tarixiy Topshiriq", Description = "Tarixiy mavzuda topshiriq muhokama", QuestionCount = 10, StartTime = new DateTime(2023, 9, 8, 15, 0, 0), EndTime = new DateTime(2023, 9, 8, 16, 0, 0), UserId = 4, ContentCategoryId = 2 },
            new Quiz { Id = 5, Name = "Kimyo Testi", Description = "Kimyo fanidan malakalaringizni sinovlash testi", QuestionCount = 18, StartTime = new DateTime(2023, 9, 9, 11, 0, 0), EndTime = new DateTime(2023, 9, 9, 12, 30, 0), UserId = 5, ContentCategoryId = 2 },
            new Quiz { Id = 6, Name = "Biologya Topshiriq", Description = "Biologya mavzusida topshiriq muhokama", QuestionCount = 12, StartTime = new DateTime(2023, 9, 10, 16, 0, 0), EndTime = new DateTime(2023, 9, 10, 17, 0, 0), UserId = 1, ContentCategoryId = 3 },
            new Quiz { Id = 7, Name = "Chet Tillari Sinovi", Description = "Chet tillari bilimlarini sinovlash testi", QuestionCount = 22, StartTime = new DateTime(2023, 9, 11, 9, 30, 0), EndTime = new DateTime(2023, 9, 11, 10, 45, 0), UserId = 2, ContentCategoryId = 3 },
            new Quiz { Id = 8, Name = "Geografiya Fan Testi", Description = "Geografiya fanidan malakalaringizni sinovlash testi", QuestionCount = 16, StartTime = new DateTime(2023, 9, 12, 14, 0, 0), EndTime = new DateTime(2023, 9, 12, 15, 30, 0), UserId = 3, ContentCategoryId = 3 },
            new Quiz { Id = 9, Name = "Informatika Sinovi", Description = "Informatika fanidan sinovlash testi", QuestionCount = 20, StartTime = new DateTime(2023, 9, 13, 10, 0, 0), EndTime = new DateTime(2023, 9, 13, 11, 30, 0), UserId = 4, ContentCategoryId = 4 },
            new Quiz { Id = 10, Name = "Dunyo Adabiyoti Testi", Description = "Dunyo adabiyoti mavzusida sinovlash testi", QuestionCount = 15, StartTime = new DateTime(2023, 9, 14, 15, 0, 0), EndTime = new DateTime(2023, 9, 15, 15, 0, 0), UserId = 5, ContentCategoryId = 4 },
            new Quiz { Id = 11, Name = "Islom Tarixi", Description = "Islom tarixi mavzusida sinovlash testi", QuestionCount = 20, StartTime = new DateTime(2023, 9, 15, 9, 0, 0), EndTime = new DateTime(2023, 9, 15, 10, 30, 0), UserId = 1, ContentCategoryId = 5 },
            new Quiz { Id = 12, Name = "Quron Tafsiri", Description = "Quron tafsiri bo'yicha sinovlash testi", QuestionCount = 15, StartTime = new DateTime(2023, 9, 16, 14, 0, 0), EndTime = new DateTime(2023, 9, 16, 15, 30, 0), UserId = 2, ContentCategoryId = 5 },
            new Quiz { Id = 13, Name = "Namoz Vaqtlari", Description = "Namoz vaqtlari mavzusida sinovlash testi", QuestionCount = 25, StartTime = new DateTime(2023, 9, 17, 10, 0, 0), EndTime = new DateTime(2023, 9, 17, 11, 30, 0), UserId = 3, ContentCategoryId = 2 },
            new Quiz { Id = 14, Name = "Islom Adablari", Description = "Islom adabiyotidan test sinovlash testi", QuestionCount = 10, StartTime = new DateTime(2023, 9, 18, 15, 0, 0), EndTime = new DateTime(2023, 9, 18, 16, 0, 0), UserId = 4, ContentCategoryId = 2 },
            new Quiz { Id = 15, Name = "Islom Ahkomi", Description = "Islom ahkomi bo'yicha sinovlash testi", QuestionCount = 18, StartTime = new DateTime(2023, 9, 19, 11, 0, 0), EndTime = new DateTime(2023, 9, 19, 12, 30, 0), UserId = 5, ContentCategoryId = 3 },
            new Quiz { Id = 16, Name = "Ramazon Vaqtlari", Description = "Ramazon oyida iftor va suhoq vaqtlari", QuestionCount = 12, StartTime = new DateTime(2023, 9, 20, 16, 0, 0), EndTime = new DateTime(2023, 9, 20, 17, 0, 0), UserId = 1, ContentCategoryId = 5 },
            new Quiz { Id = 17, Name = "Islom Falsafasi", Description = "Islom falsafasi mavzusida sinovlash testi", QuestionCount = 22, StartTime = new DateTime(2023, 9, 21, 9, 30, 0), EndTime = new DateTime(2023, 9, 21, 10, 45, 0), UserId = 2, ContentCategoryId = 1 },
            new Quiz { Id = 18, Name = "Hadislar", Description = "Islom hadislari va qissalari", QuestionCount = 16, StartTime = new DateTime(2023, 9, 22, 14, 0, 0), EndTime = new DateTime(2023, 9, 22, 15, 30, 0), UserId = 3, ContentCategoryId = 3 },
            new Quiz { Id = 19, Name = "Islom Ma'rifati", Description = "Islom ma'rifati va ta'limi", QuestionCount = 20, StartTime = new DateTime(2023, 9, 23, 10, 0, 0), EndTime = new DateTime(2023, 9, 23, 11, 30, 0), UserId = 4, ContentCategoryId = 2 },
            new Quiz { Id = 20, Name = "Islom Ibadatlari", Description = "Islom ibadatlari va amaliyotlari", QuestionCount = 15, StartTime = new DateTime(2023, 9, 24, 15, 0, 0), EndTime = new DateTime(2023, 9, 24, 16, 30, 0), UserId = 5, ContentCategoryId = 2 }
        );

        modelBuilder.Entity<Question>().HasData(
            new Question { Id = 1, Text = "Islomning besh asosiy rukni nima?" },
            new Question { Id = 2, Text = "Islom ramzlari qaysi rangda?" },
            new Question { Id = 3, Text = "Quron necha juzda bo'lgan?" },
            new Question { Id = 4, Text = "Namoz qancha marta o'qiladi?" },
            new Question { Id = 5, Text = "Islomda o'qish qachon boshlanadi?" },
            new Question { Id = 6, Text = "Quron kim tomonidan o'qilgan?" },
            new Question { Id = 7, Text = "Islomning besh rukni nima?" },
            new Question { Id = 8, Text = "Namoz o'qish tartibi qanday?" },
            new Question { Id = 9, Text = "Namozni necha rakat o'qish kerak?" },
            new Question { Id = 10, Text = "Islomda qachon roza tutiladi?" },
            new Question { Id = 12, Text = "Muhammad (S.A.V.)ning tug'ilgan yili va kunlari nima edi?" },
            new Question { Id = 13, Text = "Muhammad (S.A.V.) qaysi oilada tug'ilgan edi?" },
            new Question { Id = 14, Text = "Muhammad (S.A.V.) qaysi kitobni o'qigan edilar?" },
            new Question { Id = 15, Text = "Muhammad (S.A.V.) hayotining boshlang'ich davri qanday o'tdi?" },
            new Question { Id = 16, Text = "Muhammad (S.A.V.)ga nima chin qilindi?" },
            new Question { Id = 17, Text = "Muhammad (S.A.V.) O'rtacha Vakillar kuni qanday o'tgan?" },
            new Question { Id = 18, Text = "Muhammad (S.A.V.) qancha yillik bo'lganlarida peygambar bo'lishdi?" },
            new Question { Id = 19, Text = "Muhammad (S.A.V.) qachon vafot etdi?" },
            new Question { Id = 20, Text = "Muhammad (S.A.V.) nima yaratdi?" }
        );

        modelBuilder.Entity<QuizQuestion>().HasData(
            new QuizQuestion { Id = 1, QuizId = 1, QuestionId = 1 },
            new QuizQuestion { Id = 2, QuizId = 1, QuestionId = 2 },
            new QuizQuestion { Id = 3, QuizId = 1, QuestionId = 3 },
            new QuizQuestion { Id = 4, QuizId = 1, QuestionId = 4 },
            new QuizQuestion { Id = 5, QuizId = 1, QuestionId = 5 },
            new QuizQuestion { Id = 6, QuizId = 1, QuestionId = 6 },
            new QuizQuestion { Id = 7, QuizId = 1, QuestionId = 7 },
            new QuizQuestion { Id = 8, QuizId = 1, QuestionId = 8 },
            new QuizQuestion { Id = 9, QuizId = 1, QuestionId = 9 },
            new QuizQuestion { Id = 10, QuizId = 1, QuestionId = 10 }, 
            new QuizQuestion { Id = 12, QuizId = 1, QuestionId = 12 },
            new QuizQuestion { Id = 13, QuizId = 1, QuestionId = 13 },
            new QuizQuestion { Id = 14, QuizId = 1, QuestionId = 14 },
            new QuizQuestion { Id = 15, QuizId = 1, QuestionId = 15 },
            new QuizQuestion { Id = 16, QuizId = 1, QuestionId = 16 },
            new QuizQuestion { Id = 17, QuizId = 1, QuestionId = 17 },
            new QuizQuestion { Id = 18, QuizId = 1, QuestionId = 18 },
            new QuizQuestion { Id = 19, QuizId = 1, QuestionId = 19 },
            new QuizQuestion { Id = 20, QuizId = 1, QuestionId = 20 }
        );
        modelBuilder.Entity<Answer>().HasData(
            new Answer { Id = 1, Text = "To'g'ri" },
            new Answer { Id = 2, Text = "Noto'g'ri" },
            new Answer { Id = 3, Text = "Ishonchli javob" },
            new Answer { Id = 4, Text = "Noto'g'ri javob" },
            new Answer { Id = 5, Text = "Ha" },
            new Answer { Id = 6, Text = "Yo'q" },
            new Answer { Id = 7, Text = "O'zgarmas" },
            new Answer { Id = 8, Text = "Boshqa variant" },
            new Answer { Id = 9, Text = "Tasdiqlandi" },
            new Answer { Id = 10, Text = "Tasdiqlanmadi" },
            new Answer { Id = 11, Text = "Yaxshi" },
            new Answer { Id = 12, Text = "Yomon" },
            new Answer { Id = 13, Text = "O'rniga" },
            new Answer { Id = 14, Text = "Boshqa" },
            new Answer { Id = 15, Text = "Ma'lum emas" },
            new Answer { Id = 16, Text = "Haqiqatan ham, to'g'ri" },
            new Answer { Id = 17, Text = "Yo'q, noto'g'ri" },
            new Answer { Id = 18, Text = "Aniqlanmagan" },
            new Answer { Id = 19, Text = "Ijtinob etish kerak" },
            new Answer { Id = 20, Text = "O'zgarmas, sabab ma'lum emas" },
            new Answer { Id = 21, Text = "To'g'ri, yomon" },
            new Answer { Id = 22, Text = "Yo'q, yaxshi emas" },
            new Answer { Id = 23, Text = "Aniqlanmagan, yomon" },
            new Answer { Id = 24, Text = "Boshqa variant" },
            new Answer { Id = 25, Text = "Tushunmadim" }
        );

        modelBuilder.Entity<QuestionAnswer>().HasData(
            new QuestionAnswer { Id = 1, AnswerId = 16, QuestionId = 1, UserId = 1, QuizId = 1, IsTrue = true },
            new QuestionAnswer { Id = 2, AnswerId = 17, QuestionId = 2, UserId = 2, QuizId = 8, IsTrue = false },
            new QuestionAnswer { Id = 3, AnswerId = 18, QuestionId = 3, UserId = 3, QuizId = 1, IsTrue = false },
            new QuestionAnswer { Id = 4, AnswerId = 19, QuestionId = 4, UserId = 4, QuizId = 3, IsTrue = true },
            new QuestionAnswer { Id = 5, AnswerId = 20, QuestionId = 5, UserId = 5, QuizId = 5, IsTrue = false },
            new QuestionAnswer { Id = 6, AnswerId = 21, QuestionId = 6, UserId = 1, QuizId = 2, IsTrue = false },
            new QuestionAnswer { Id = 7, AnswerId = 22, QuestionId = 7, UserId = 2, QuizId = 2, IsTrue = true },
            new QuestionAnswer { Id = 8, AnswerId = 23, QuestionId = 8, UserId = 3, QuizId = 2, IsTrue = false },
            new QuestionAnswer { Id = 9, AnswerId = 24, QuestionId = 9, UserId = 4, QuizId = 2, IsTrue = false },
            new QuestionAnswer { Id = 10, AnswerId = 25, QuestionId = 10, UserId = 5, QuizId = 2, IsTrue = true },
            new QuestionAnswer { Id = 11, AnswerId = 6, QuestionId = 1, UserId = 7, QuizId = 3, IsTrue = true },
            new QuestionAnswer { Id = 12, AnswerId = 7, QuestionId = 2, UserId = 7, QuizId = 3, IsTrue = false },
            new QuestionAnswer { Id = 13, AnswerId = 8, QuestionId = 3, UserId = 8, QuizId = 7, IsTrue = false },
            new QuestionAnswer { Id = 14, AnswerId = 9, QuestionId = 4, UserId = 9, QuizId = 3, IsTrue = true },
            new QuestionAnswer { Id = 15, AnswerId = 2, QuestionId = 5, UserId = 10, QuizId = 3, IsTrue = false },
            new QuestionAnswer { Id = 16, AnswerId = 1, QuestionId = 6, UserId = 8, QuizId = 4, IsTrue = false },
            new QuestionAnswer { Id = 17, AnswerId = 2, QuestionId = 7, UserId = 7, QuizId = 4, IsTrue = true },
            new QuestionAnswer { Id = 18, AnswerId = 3, QuestionId = 8, UserId = 8, QuizId = 4, IsTrue = false },
            new QuestionAnswer { Id = 19, AnswerId = 4, QuestionId = 9, UserId = 9, QuizId = 4, IsTrue = false },
            new QuestionAnswer { Id = 20, AnswerId = 5, QuestionId = 10, UserId = 10, QuizId = 4, IsTrue = true }
        );
        modelBuilder.Entity<Content>().HasData(
            new Content { Id = 1, ContentCategoryId = 1 },
            new Content { Id = 2, ContentCategoryId = 1 },
            new Content { Id = 3, ContentCategoryId = 1 },
            new Content { Id = 4, ContentCategoryId = 1 },
            new Content { Id = 5, ContentCategoryId = 1 },
            new Content { Id = 6, ContentCategoryId = 1 }
        );

        modelBuilder.Entity<Article>().HasData(
            new Article { Id = 1, Text = "Bu birinchi maqola matni.", ContentId = 1 },
            new Article { Id = 2, Text = "Ushbu maqolada muhim muddatlar haqida gaplashiladi.", ContentId = 1 },
            new Article { Id = 3, Text = "Maqolada til to'g'risida muhim ma'lumotlar berilgan.", ContentId = 1 },
            new Article { Id = 4, Text = "Bu dasturni o'rganish uchun yaxshi manba.", ContentId = 2 },
            new Article { Id = 5, Text = "Maqola yozishning eng asosiy qoidalari.", ContentId = 2 },
            new Article { Id = 6, Text = "Bu esa test matni.", ContentId = 3 },
            new Article { Id = 7, Text = "Matn tahrirlovchilari uchun eng yaxshi darslik.", ContentId = 3 },
            new Article { Id = 8, Text = "Maqolada boshqa muhim ma'lumotlar.", ContentId = 3 },
            new Article { Id = 9, Text = "Yozilgan maqolaning tafsili.", ContentId = 4 },
            new Article { Id = 10, Text = "Tafsiliroq ma'lumotlar uchun yuqoridagi manbani o'qishingiz mumkin.", ContentId = 4 },
            new Article { Id = 11, Text = "Yangi maqola", ContentId = 5 },
            new Article { Id = 12, Text = "Maqolani tashkil etish", ContentId = 5 },
            new Article { Id = 13, Text = "So'nggi yangiliklar", ContentId = 6 },
            new Article { Id = 14, Text = "Sport yangiliklari", ContentId = 6 },
            new Article { Id = 15, Text = "Biznes yangiliklar", ContentId = 6 }
        );
        modelBuilder.Entity<UserArticle>().HasData(
            new UserArticle { Id = 1, UserId = 1, ArticleId = 11 },
            new UserArticle { Id = 2, UserId = 2, ArticleId = 11 },
            new UserArticle { Id = 3, UserId = 3, ArticleId = 12 },
            new UserArticle { Id = 4, UserId = 4, ArticleId = 13 },
            new UserArticle { Id = 5, UserId = 5, ArticleId = 14 },
            new UserArticle { Id = 7, UserId = 7, ArticleId = 11 },
            new UserArticle { Id = 8, UserId = 8, ArticleId = 13 },
            new UserArticle { Id = 9, UserId = 9, ArticleId = 14 },
            new UserArticle { Id = 10, UserId = 10, ArticleId = 15 },
            new UserArticle { Id = 12, UserId = 12, ArticleId = 2 }
        );
        #endregion
    }
}
