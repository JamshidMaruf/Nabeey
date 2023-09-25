using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nabeey.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
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
                    Text = table.Column<string>(type: "text", nullable: true),
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
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    ImageId = table.Column<long>(type: "bigint", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                table: "Books",
                columns: new[] { "Id", "AssetId", "Author", "CreatedAt", "Description", "IsDeleted", "Text", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 3L, null, "George Orwell", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3337), "Dystopian novel", false, "Text 3", "1984", null },
                    { 4L, null, "Harper Lee", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3343), "Classic novel", false, "Text 4", "To Kill a Mockingbird", null },
                    { 5L, null, "F. Scott Fitzgerald", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3344), "American classic", false, "Text 5", "The Great Gatsby", null },
                    { 6L, null, "Jane Austen", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3345), "Romantic novel", false, "Text 6", "Pride and Prejudice", null },
                    { 7L, null, "J.D. Salinger", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3347), "Coming-of-age novel", false, "Text 7", "The Catcher in the Rye", null },
                    { 8L, null, "J.R.R. Tolkien", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3348), "Epic fantasy", false, "Text 8", "Lord of the Rings", null },
                    { 9L, null, "J.K. Rowling", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3349), "Fantasy novel", false, "Text 9", "Harry Potter and the Sorcerer's Stone", null },
                    { 10L, null, "J.R.R. Tolkien", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3350), "Fantasy adventure", false, "Text 10", "The Hobbit", null },
                    { 11L, null, "Charles Dickens", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3351), "Roman klassikasi", false, "Matni 11", "Oliver Twist", null },
                    { 12L, null, "Arthyr Konan Doil", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3353), "Mashhur detektiv qissalari", false, "Matni 12", "Sherlok Holms", null },
                    { 13L, null, "Fyodor Dostoyevski", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3354), "Rus roman klassikasi", false, "Matni 13", "Qo'shiqchi", null },
                    { 14L, null, "H. P. Lovecraft", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3355), "Fantastika", false, "Matni 14", "Zulumotlar va qo'rqinlar vaqti", null },
                    { 15L, null, "Stephen King", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3356), "G'azablandiruvchi roman", false, "Matni 15", "Qorquv", null },
                    { 16L, null, "Herman Melville", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3357), "Qayiq ko'prik qirg'ishi", false, "Matni 16", "Mobi-Dik", null },
                    { 17L, null, "Munis Xo'ja", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3358), "Xalq qahramoni tarixiy roman", false, "Matni 17", "Andijonlik", null },
                    { 18L, null, "Alexander Duma", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3359), "Maktab roman klassikasi", false, "Matni 18", "Sulton Kuzo", null },
                    { 19L, null, "Edgar Allan Poe", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3361), "G'azallar", false, "Matni 19", "Qo'rqinchli g'azal", null },
                    { 20L, null, "Ivan Denisovich", new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3362), "Olam shekillari roman", false, "Matni 20", "Bir kunda", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AssetId", "CreatedAt", "Email", "FirstName", "IsDeleted", "LastName", "PasswordHash", "Phone", "UpdatedAt", "UserRole" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3522), "imona.kabirova@example.com", "Imona", false, "Kabirova", "a", "9001234567", null, 1 },
                    { 2L, null, new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3527), "jamshid.zayniev@example.com", "Jamshid", false, "Zayniev", "a", "9007654321", null, 1 },
                    { 3L, null, new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3529), "anastasiya.tomchuk@example.com", "Anastasiya", false, "Tomchuk", "a", "9009876543", null, 1 },
                    { 4L, null, new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3531), "iskandar.kodirov@example.com", "Iskandar", false, "Kodirov", "a", "9012345678", null, 1 },
                    { 5L, null, new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3532), "nodirshax.allanazarov@example.com", "Nodirshax", false, "Allanazarov", "a", "9012345679", null, 1 },
                    { 7L, null, new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3533), "asilbek.abdurashidov@example.com", "Asilbek", false, "Abdurashidov", "a", "9012345679", null, 1 },
                    { 8L, null, new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3535), "jasurbek.ergashev@example.com", "Jasurbek", false, "Ergashev", "a", "9012345680", null, 1 },
                    { 9L, null, new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3536), "takhmina.saidova@example.com", "Takhmina", false, "Saidova", "a", "9012345681", null, 1 },
                    { 10L, null, new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3538), "asadbek.qarshiyev@example.com", "Asadbek", false, "Qarshiyev", "a", "9012345682", null, 1 },
                    { 11L, null, new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3539), "sardor.sohinazarov@example.com", "Sardor", false, "Sohinazarov", "a", "9012345683", null, 1 },
                    { 12L, null, new DateTime(2023, 9, 25, 21, 16, 4, 313, DateTimeKind.Utc).AddTicks(3540), "raykhona.isroilova@example.com", "Raykhona", false, "Isroilova", "a", "9012345684", null, 1 }
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
