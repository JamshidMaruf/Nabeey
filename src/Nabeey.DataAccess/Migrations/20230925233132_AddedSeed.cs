using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nabeey.DataAccess.Migrations
{
    public partial class AddedSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: true),
                    AssetId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AssetId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: true),
                    AssetId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    UserRole = table.Column<int>(type: "integer", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContentCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contents_ContentCategories_ContentCategoryId",
                        column: x => x.ContentCategoryId,
                        principalTable: "ContentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    QuestionCount = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ContentCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizzes_ContentCategories_ContentCategoryId",
                        column: x => x.ContentCategoryId,
                        principalTable: "ContentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quizzes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: true),
                    ContentId = table.Column<long>(type: "bigint", nullable: false),
                    ImageId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Assets_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Articles_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentAudios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ContentId = table.Column<long>(type: "bigint", nullable: false),
                    AudioId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentAudios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentAudios_Assets_AudioId",
                        column: x => x.AudioId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContentAudios_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentBooks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    ContentId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentBooks_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentVideos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContentId = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentVideos_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContentVideos_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswers",
                columns: table => new
                {
                    AnswerId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    QuizId = table.Column<long>(type: "bigint", nullable: false),
                    IsTrue = table.Column<bool>(type: "boolean", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswers", x => new { x.QuestionId, x.AnswerId });
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    QuizId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => new { x.QuizId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserArticles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ArticleId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserArticles_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserArticles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "AssetId", "CreatedAt", "IsDeleted", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8574), false, "To'g'ri", null },
                    { 2L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8576), false, "Noto'g'ri", null },
                    { 3L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8577), false, "Ishonchli javob", null },
                    { 4L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8578), false, "Noto'g'ri javob", null },
                    { 5L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8578), false, "Ha", null },
                    { 6L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8579), false, "Yo'q", null },
                    { 7L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8579), false, "O'zgarmas", null },
                    { 8L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8580), false, "Boshqa variant", null },
                    { 9L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8581), false, "Tasdiqlandi", null },
                    { 10L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8581), false, "Tasdiqlanmadi", null },
                    { 11L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8582), false, "Yaxshi", null },
                    { 12L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8582), false, "Yomon", null },
                    { 13L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8583), false, "O'rniga", null },
                    { 14L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8584), false, "Boshqa", null },
                    { 15L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8584), false, "Ma'lum emas", null },
                    { 16L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8585), false, "Haqiqatan ham, to'g'ri", null },
                    { 17L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8585), false, "Yo'q, noto'g'ri", null },
                    { 18L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8586), false, "Aniqlanmagan", null },
                    { 19L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8587), false, "Ijtinob etish kerak", null },
                    { 20L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8587), false, "O'zgarmas, sabab ma'lum emas", null },
                    { 21L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8588), false, "To'g'ri, yomon", null },
                    { 22L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8588), false, "Yo'q, yaxshi emas", null },
                    { 23L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8589), false, "Aniqlanmagan, yomon", null },
                    { 24L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8590), false, "Boshqa variant", null },
                    { 25L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8590), false, "Tushunmadim", null }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AssetId", "Author", "CreatedAt", "Description", "IsDeleted", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 3L, null, "George Orwell", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8135), "Dystopian novel", false, "1984", null },
                    { 4L, null, "Harper Lee", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8141), "Classic novel", false, "To Kill a Mockingbird", null },
                    { 5L, null, "F. Scott Fitzgerald", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8142), "American classic", false, "The Great Gatsby", null },
                    { 6L, null, "Jane Austen", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8143), "Romantic novel", false, "Pride and Prejudice", null },
                    { 7L, null, "J.D. Salinger", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8144), "Coming-of-age novel", false, "The Catcher in the Rye", null },
                    { 8L, null, "J.R.R. Tolkien", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8145), "Epic fantasy", false, "Lord of the Rings", null },
                    { 9L, null, "J.K. Rowling", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8145), "Fantasy novel", false, "Harry Potter and the Sorcerer's Stone", null },
                    { 10L, null, "J.R.R. Tolkien", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8146), "Fantasy adventure", false, "The Hobbit", null },
                    { 11L, null, "Charles Dickens", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8147), "Roman klassikasi", false, "Oliver Twist", null },
                    { 12L, null, "Arthyr Konan Doil", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8148), "Mashhur detektiv qissalari", false, "Sherlok Holms", null },
                    { 13L, null, "Fyodor Dostoyevski", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8149), "Rus roman klassikasi", false, "Qo'shiqchi", null },
                    { 14L, null, "H. P. Lovecraft", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8150), "Fantastika", false, "Zulumotlar va qo'rqinlar vaqti", null },
                    { 15L, null, "Stephen King", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8150), "G'azablandiruvchi roman", false, "Qorquv", null },
                    { 16L, null, "Herman Melville", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8151), "Qayiq ko'prik qirg'ishi", false, "Mobi-Dik", null },
                    { 17L, null, "Munis Xo'ja", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8152), "Xalq qahramoni tarixiy roman", false, "Andijonlik", null },
                    { 18L, null, "Alexander Duma", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8153), "Maktab roman klassikasi", false, "Sulton Kuzo", null },
                    { 19L, null, "Edgar Allan Poe", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8154), "G'azallar", false, "Qo'rqinchli g'azal", null },
                    { 20L, null, "Ivan Denisovich", new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8155), "Olam shekillari roman", false, "Bir kunda", null }
                });

            migrationBuilder.InsertData(
                table: "ContentCategories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8402), false, "Darsliklar", null },
                    { 2L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8405), false, "Romanlar", null },
                    { 3L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8406), false, "Maqolalar", null },
                    { 4L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8407), false, "She'rlar", null },
                    { 5L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8407), false, "Tadbirlar", null }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "AssetId", "CreatedAt", "IsDeleted", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8481), false, "Islomning besh asosiy rukni nima?", null },
                    { 2L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8482), false, "Islom ramzlari qaysi rangda?", null },
                    { 3L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8483), false, "Quron necha juzda bo'lgan?", null },
                    { 4L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8484), false, "Namoz qancha marta o'qiladi?", null },
                    { 5L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8484), false, "Islomda o'qish qachon boshlanadi?", null },
                    { 6L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8485), false, "Quron kim tomonidan o'qilgan?", null },
                    { 7L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8486), false, "Islomning besh rukni nima?", null },
                    { 8L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8486), false, "Namoz o'qish tartibi qanday?", null },
                    { 9L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8487), false, "Namozni necha rakat o'qish kerak?", null },
                    { 10L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8487), false, "Islomda qachon roza tutiladi?", null },
                    { 12L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8488), false, "Muhammad (S.A.V.)ning tug'ilgan yili va kunlari nima edi?", null },
                    { 13L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8489), false, "Muhammad (S.A.V.) qaysi oilada tug'ilgan edi?", null },
                    { 14L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8489), false, "Muhammad (S.A.V.) qaysi kitobni o'qigan edilar?", null },
                    { 15L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8490), false, "Muhammad (S.A.V.) hayotining boshlang'ich davri qanday o'tdi?", null },
                    { 16L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8491), false, "Muhammad (S.A.V.)ga nima chin qilindi?", null },
                    { 17L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8491), false, "Muhammad (S.A.V.) O'rtacha Vakillar kuni qanday o'tgan?", null },
                    { 18L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8492), false, "Muhammad (S.A.V.) qancha yillik bo'lganlarida peygambar bo'lishdi?", null },
                    { 19L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8493), false, "Muhammad (S.A.V.) qachon vafot etdi?", null },
                    { 20L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8493), false, "Muhammad (S.A.V.) nima yaratdi?", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AssetId", "CreatedAt", "Email", "FirstName", "IsDeleted", "LastName", "PasswordHash", "Phone", "UpdatedAt", "UserRole" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8323), "imona.kabirova@example.com", "Imona", false, "Kabirova", "a", "9001234567", null, 1 },
                    { 2L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8328), "jamshid.zayniev@example.com", "Jamshid", false, "Zayniev", "a", "9007654321", null, 1 },
                    { 3L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8329), "anastasiya.tomchuk@example.com", "Anastasiya", false, "Tomchuk", "a", "9009876543", null, 1 },
                    { 4L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8331), "iskandar.kodirov@example.com", "Iskandar", false, "Kodirov", "a", "9012345678", null, 1 },
                    { 5L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8373), "nodirshax.allanazarov@example.com", "Nodirshax", false, "Allanazarov", "a", "9012345679", null, 1 },
                    { 7L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8374), "asilbek.abdurashidov@example.com", "Asilbek", false, "Abdurashidov", "a", "9012345679", null, 1 },
                    { 8L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8375), "jasurbek.ergashev@example.com", "Jasurbek", false, "Ergashev", "a", "9012345680", null, 1 },
                    { 9L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8377), "takhmina.saidova@example.com", "Takhmina", false, "Saidova", "a", "9012345681", null, 1 },
                    { 10L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8378), "asadbek.qarshiyev@example.com", "Asadbek", false, "Qarshiyev", "a", "9012345682", null, 1 },
                    { 11L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8379), "sardor.sohinazarov@example.com", "Sardor", false, "Sohinazarov", "a", "9012345683", null, 1 },
                    { 12L, null, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8380), "raykhona.isroilova@example.com", "Raykhona", false, "Isroilova", "a", "9012345684", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Contents",
                columns: new[] { "Id", "ContentCategoryId", "CreatedAt", "IsDeleted", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8687), false, null },
                    { 2L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8689), false, null },
                    { 3L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8689), false, null },
                    { 4L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8690), false, null },
                    { 5L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8691), false, null },
                    { 6L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8691), false, null }
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "Id", "ContentCategoryId", "CreatedAt", "Description", "EndTime", "IsDeleted", "Name", "QuestionCount", "StartTime", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8424), "Matematika mavzusida umumiy test", new DateTime(2023, 9, 5, 10, 30, 0, 0, DateTimeKind.Unspecified), false, "Matematika Testi", 20, new DateTime(2023, 9, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), null, 1L },
                    { 2L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8433), "Ingliz tili bilimlarini sinovlash testi", new DateTime(2023, 9, 6, 15, 30, 0, 0, DateTimeKind.Unspecified), false, "Ingliz Tilini Testlash", 15, new DateTime(2023, 9, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), null, 2L },
                    { 3L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8435), "Fizika fanidan malakalaringizni sinovlash testi", new DateTime(2023, 9, 7, 11, 30, 0, 0, DateTimeKind.Unspecified), false, "Fizika Fan Testi", 25, new DateTime(2023, 9, 7, 10, 0, 0, 0, DateTimeKind.Unspecified), null, 3L },
                    { 4L, 2L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8436), "Tarixiy mavzuda topshiriq muhokama", new DateTime(2023, 9, 8, 16, 0, 0, 0, DateTimeKind.Unspecified), false, "Tarixiy Topshiriq", 10, new DateTime(2023, 9, 8, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 4L },
                    { 5L, 2L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8438), "Kimyo fanidan malakalaringizni sinovlash testi", new DateTime(2023, 9, 9, 12, 30, 0, 0, DateTimeKind.Unspecified), false, "Kimyo Testi", 18, new DateTime(2023, 9, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), null, 5L },
                    { 6L, 3L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8439), "Biologya mavzusida topshiriq muhokama", new DateTime(2023, 9, 10, 17, 0, 0, 0, DateTimeKind.Unspecified), false, "Biologya Topshiriq", 12, new DateTime(2023, 9, 10, 16, 0, 0, 0, DateTimeKind.Unspecified), null, 1L },
                    { 7L, 3L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8441), "Chet tillari bilimlarini sinovlash testi", new DateTime(2023, 9, 11, 10, 45, 0, 0, DateTimeKind.Unspecified), false, "Chet Tillari Sinovi", 22, new DateTime(2023, 9, 11, 9, 30, 0, 0, DateTimeKind.Unspecified), null, 2L },
                    { 8L, 3L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8442), "Geografiya fanidan malakalaringizni sinovlash testi", new DateTime(2023, 9, 12, 15, 30, 0, 0, DateTimeKind.Unspecified), false, "Geografiya Fan Testi", 16, new DateTime(2023, 9, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), null, 3L },
                    { 9L, 4L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8445), "Informatika fanidan sinovlash testi", new DateTime(2023, 9, 13, 11, 30, 0, 0, DateTimeKind.Unspecified), false, "Informatika Sinovi", 20, new DateTime(2023, 9, 13, 10, 0, 0, 0, DateTimeKind.Unspecified), null, 4L },
                    { 10L, 4L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8446), "Dunyo adabiyoti mavzusida sinovlash testi", new DateTime(2023, 9, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), false, "Dunyo Adabiyoti Testi", 15, new DateTime(2023, 9, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 5L },
                    { 11L, 5L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8447), "Islom tarixi mavzusida sinovlash testi", new DateTime(2023, 9, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), false, "Islom Tarixi", 20, new DateTime(2023, 9, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), null, 1L },
                    { 12L, 5L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8449), "Quron tafsiri bo'yicha sinovlash testi", new DateTime(2023, 9, 16, 15, 30, 0, 0, DateTimeKind.Unspecified), false, "Quron Tafsiri", 15, new DateTime(2023, 9, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), null, 2L },
                    { 13L, 2L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8450), "Namoz vaqtlari mavzusida sinovlash testi", new DateTime(2023, 9, 17, 11, 30, 0, 0, DateTimeKind.Unspecified), false, "Namoz Vaqtlari", 25, new DateTime(2023, 9, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), null, 3L },
                    { 14L, 2L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8451), "Islom adabiyotidan test sinovlash testi", new DateTime(2023, 9, 18, 16, 0, 0, 0, DateTimeKind.Unspecified), false, "Islom Adablari", 10, new DateTime(2023, 9, 18, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 4L },
                    { 15L, 3L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8453), "Islom ahkomi bo'yicha sinovlash testi", new DateTime(2023, 9, 19, 12, 30, 0, 0, DateTimeKind.Unspecified), false, "Islom Ahkomi", 18, new DateTime(2023, 9, 19, 11, 0, 0, 0, DateTimeKind.Unspecified), null, 5L },
                    { 16L, 5L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8454), "Ramazon oyida iftor va suhoq vaqtlari", new DateTime(2023, 9, 20, 17, 0, 0, 0, DateTimeKind.Unspecified), false, "Ramazon Vaqtlari", 12, new DateTime(2023, 9, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), null, 1L },
                    { 17L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8456), "Islom falsafasi mavzusida sinovlash testi", new DateTime(2023, 9, 21, 10, 45, 0, 0, DateTimeKind.Unspecified), false, "Islom Falsafasi", 22, new DateTime(2023, 9, 21, 9, 30, 0, 0, DateTimeKind.Unspecified), null, 2L },
                    { 18L, 3L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8457), "Islom hadislari va qissalari", new DateTime(2023, 9, 22, 15, 30, 0, 0, DateTimeKind.Unspecified), false, "Hadislar", 16, new DateTime(2023, 9, 22, 14, 0, 0, 0, DateTimeKind.Unspecified), null, 3L },
                    { 19L, 2L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8458), "Islom ma'rifati va ta'limi", new DateTime(2023, 9, 23, 11, 30, 0, 0, DateTimeKind.Unspecified), false, "Islom Ma'rifati", 20, new DateTime(2023, 9, 23, 10, 0, 0, 0, DateTimeKind.Unspecified), null, 4L },
                    { 20L, 2L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8460), "Islom ibadatlari va amaliyotlari", new DateTime(2023, 9, 24, 16, 30, 0, 0, DateTimeKind.Unspecified), false, "Islom Ibadatlari", 15, new DateTime(2023, 9, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 5L }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "ContentId", "CreatedAt", "ImageId", "IsDeleted", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8707), null, false, "Bu birinchi maqola matni.", null },
                    { 2L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8708), null, false, "Ushbu maqolada muhim muddatlar haqida gaplashiladi.", null },
                    { 3L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8709), null, false, "Maqolada til to'g'risida muhim ma'lumotlar berilgan.", null },
                    { 4L, 2L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8710), null, false, "Bu dasturni o'rganish uchun yaxshi manba.", null },
                    { 5L, 2L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8710), null, false, "Maqola yozishning eng asosiy qoidalari.", null },
                    { 6L, 3L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8711), null, false, "Bu esa test matni.", null },
                    { 7L, 3L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8712), null, false, "Matn tahrirlovchilari uchun eng yaxshi darslik.", null },
                    { 8L, 3L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8712), null, false, "Maqolada boshqa muhim ma'lumotlar.", null },
                    { 9L, 4L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8713), null, false, "Yozilgan maqolaning tafsili.", null },
                    { 10L, 4L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8714), null, false, "Tafsiliroq ma'lumotlar uchun yuqoridagi manbani o'qishingiz mumkin.", null },
                    { 11L, 5L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8715), null, false, "Yangi maqola", null },
                    { 12L, 5L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8715), null, false, "Maqolani tashkil etish", null },
                    { 13L, 6L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8716), null, false, "So'nggi yangiliklar", null },
                    { 14L, 6L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8717), null, false, "Sport yangiliklari", null },
                    { 15L, 6L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8717), null, false, "Biznes yangiliklar", null }
                });

            migrationBuilder.InsertData(
                table: "QuestionAnswers",
                columns: new[] { "AnswerId", "QuestionId", "CreatedAt", "Id", "IsDeleted", "IsTrue", "QuizId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 6L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8624), 11L, false, true, 3L, null, 7L },
                    { 16L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8610), 1L, false, true, 1L, null, 1L },
                    { 7L, 2L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8625), 12L, false, false, 3L, null, 7L },
                    { 17L, 2L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8615), 2L, false, false, 8L, null, 2L },
                    { 8L, 3L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8657), 13L, false, false, 7L, null, 8L },
                    { 18L, 3L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8616), 3L, false, false, 1L, null, 3L },
                    { 9L, 4L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8658), 14L, false, true, 3L, null, 9L },
                    { 19L, 4L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8617), 4L, false, true, 3L, null, 4L },
                    { 2L, 5L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8659), 15L, false, false, 3L, null, 10L },
                    { 20L, 5L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8618), 5L, false, false, 5L, null, 5L },
                    { 1L, 6L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8660), 16L, false, false, 4L, null, 8L },
                    { 21L, 6L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8619), 6L, false, false, 2L, null, 1L },
                    { 2L, 7L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8661), 17L, false, true, 4L, null, 7L },
                    { 22L, 7L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8620), 7L, false, true, 2L, null, 2L },
                    { 3L, 8L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8662), 18L, false, false, 4L, null, 8L },
                    { 23L, 8L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8621), 8L, false, false, 2L, null, 3L },
                    { 4L, 9L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8663), 19L, false, false, 4L, null, 9L },
                    { 24L, 9L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8622), 9L, false, false, 2L, null, 4L },
                    { 5L, 10L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8664), 20L, false, true, 4L, null, 10L },
                    { 25L, 10L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8623), 10L, false, true, 2L, null, 5L }
                });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "QuestionId", "QuizId", "CreatedAt", "Id", "IsDeleted", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8541), 1L, false, null },
                    { 2L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8543), 2L, false, null },
                    { 3L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8544), 3L, false, null },
                    { 4L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8545), 4L, false, null },
                    { 5L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8546), 5L, false, null },
                    { 6L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8547), 6L, false, null },
                    { 7L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8547), 7L, false, null },
                    { 8L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8548), 8L, false, null },
                    { 9L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8549), 9L, false, null },
                    { 10L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8549), 10L, false, null },
                    { 12L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8550), 12L, false, null },
                    { 13L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8551), 13L, false, null },
                    { 14L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8552), 14L, false, null },
                    { 15L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8552), 15L, false, null },
                    { 16L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8553), 16L, false, null },
                    { 17L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8554), 17L, false, null },
                    { 18L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8554), 18L, false, null },
                    { 19L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8555), 19L, false, null },
                    { 20L, 1L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8556), 20L, false, null }
                });

            migrationBuilder.InsertData(
                table: "UserArticles",
                columns: new[] { "Id", "ArticleId", "CreatedAt", "IsDeleted", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1L, 11L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8734), false, null, 1L },
                    { 2L, 11L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8735), false, null, 2L },
                    { 3L, 12L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8736), false, null, 3L },
                    { 4L, 13L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8737), false, null, 4L },
                    { 5L, 14L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8738), false, null, 5L },
                    { 7L, 11L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8738), false, null, 7L },
                    { 8L, 13L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8739), false, null, 8L },
                    { 9L, 14L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8740), false, null, 9L },
                    { 10L, 15L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8740), false, null, 10L },
                    { 12L, 2L, new DateTime(2023, 9, 25, 23, 31, 31, 729, DateTimeKind.Utc).AddTicks(8741), false, null, 12L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AssetId",
                table: "Answers",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ContentId",
                table: "Articles",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ImageId",
                table: "Articles",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AssetId",
                table: "Books",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAudios_AudioId",
                table: "ContentAudios",
                column: "AudioId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAudios_ContentId",
                table: "ContentAudios",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentBooks_BookId",
                table: "ContentBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentBooks_ContentId",
                table: "ContentBooks",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ContentCategoryId",
                table: "Contents",
                column: "ContentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentVideos_AssetId",
                table: "ContentVideos",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentVideos_ContentId",
                table: "ContentVideos",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_AnswerId",
                table: "QuestionAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_QuizId",
                table: "QuestionAnswers",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_UserId",
                table: "QuestionAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AssetId",
                table: "Questions",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuestionId",
                table: "QuizQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_ContentCategoryId",
                table: "Quizzes",
                column: "ContentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_UserId",
                table: "Quizzes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticles_ArticleId",
                table: "UserArticles",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticles_UserId",
                table: "UserArticles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AssetId",
                table: "Users",
                column: "AssetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentAudios");

            migrationBuilder.DropTable(
                name: "ContentBooks");

            migrationBuilder.DropTable(
                name: "ContentVideos");

            migrationBuilder.DropTable(
                name: "QuestionAnswers");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "UserArticles");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "ContentCategories");
        }
    }
}
