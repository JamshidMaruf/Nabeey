using Nabeey.Domain.Entities.Users;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Assets;
using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities.Answers;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Entities.Certificates;
using Nabeey.Domain.Entities.QuizQuestions;
using Nabeey.Domain.Entities.QuestionAnswers;

namespace Nabeey.DataAccess.Contexts;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{ }
	public DbSet<Answer> Answers { get; set; }
	public DbSet<Article> Articles { get; set; }
	public DbSet<Asset> Assets { get; set; }
	public DbSet<Book> Books { get; set; }
	public DbSet<ContentAudio> ContentAudios { get; set; }
	public DbSet<ContentCategory> ContentCategories { get; set; }
	public DbSet<ContentVideo> ContentVideos { get; set; }
	public DbSet<Question> Questions { get; set; }
	public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
	public DbSet<Quiz> Quizzes { get; set; }
	public DbSet<QuizQuestion> QuizQuestions { get; set; }
	public DbSet<Certificate> Certificates { get; set; }
	public DbSet<User> Users { get; set; }

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
		modelBuilder.Entity<ContentAudio>().HasQueryFilter(u => !u.IsDeleted);
		modelBuilder.Entity<ContentCategory>().HasQueryFilter(u => !u.IsDeleted);
		modelBuilder.Entity<ContentVideo>().HasQueryFilter(u => !u.IsDeleted);
		modelBuilder.Entity<Question>().HasQueryFilter(u => !u.IsDeleted);
		modelBuilder.Entity<Quiz>().HasQueryFilter(u => !u.IsDeleted);
		modelBuilder.Entity<QuizQuestion>().HasQueryFilter(u => !u.IsDeleted);
		modelBuilder.Entity<QuestionAnswer>().HasQueryFilter(qa => !qa.IsDeleted);
		#endregion

		#region Many to many realationship --->>
		// Users <=> Articles
		//var userArticle = modelBuilder.Entity<UserArticle>();
		//userArticle.HasKey(ua => new { ua.UserId, ua.ArticleId });
		//userArticle.HasOne(ua => ua.User).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.UserId);
		//userArticle.HasOne(ua => ua.Article).WithMany(ua => ua.UserArticles).HasForeignKey(ua => ua.ArticleId);

		// Article and User
		modelBuilder.Entity<Article>()
		  .HasOne(a => a.User)
		  .WithMany(u => u.Articles)
		  .HasForeignKey(a => a.UserId)
		  .OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<Article>()
		  .HasOne(a => a.Category)
		  .WithMany(c => c.Articles)
		  .HasForeignKey(a => a.CategoryId)
		  .OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<User>()
			.HasOne(u => u.Asset)
			.WithOne()
			.HasForeignKey<User>(u => u.AssetId)
			.OnDelete(DeleteBehavior.SetNull);

		//Question and Asset
		modelBuilder.Entity<Question>()
		   .HasOne(q => q.Image)
		   .WithOne()
		   .HasForeignKey<Question>(q => q.ImageId);

		modelBuilder.Entity<Answer>()
		   .HasOne(a => a.Asset)
		   .WithOne()
		   .HasForeignKey<Answer>(a => a.AssetId);

		//Question and Answer
		modelBuilder.Entity<Question>()
			.HasMany(q => q.Answers)
			.WithOne(a => a.Question)
			.HasForeignKey(a => a.QuestionId);

		modelBuilder.Entity<Answer>()
			.HasOne(a => a.Question)
			.WithMany(q => q.Answers)
			.HasForeignKey(a => a.QuestionId);

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
			.HasOne(qq => qq.Quiz)
			.WithMany()
			.HasForeignKey(qq => qq.QuizId);

		modelBuilder.Entity<QuizQuestion>()
			.HasOne(qq => qq.Question)
			.WithMany()
			.HasForeignKey(qq => qq.QuestionId);
		#endregion
	}
}
