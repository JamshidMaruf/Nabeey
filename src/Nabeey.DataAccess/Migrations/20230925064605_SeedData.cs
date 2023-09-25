using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nabeey.DataAccess.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AssetId", "Author", "CreatedAt", "Description", "IsDeleted", "Text", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 3L, null, "George Orwell", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dystopian novel", false, "Text 3", "1984", null },
                    { 4L, null, "Harper Lee", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Classic novel", false, "Text 4", "To Kill a Mockingbird", null },
                    { 5L, null, "F. Scott Fitzgerald", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "American classic", false, "Text 5", "The Great Gatsby", null },
                    { 6L, null, "Jane Austen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romantic novel", false, "Text 6", "Pride and Prejudice", null },
                    { 7L, null, "J.D. Salinger", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Coming-of-age novel", false, "Text 7", "The Catcher in the Rye", null },
                    { 8L, null, "J.R.R. Tolkien", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Epic fantasy", false, "Text 8", "Lord of the Rings", null },
                    { 9L, null, "J.K. Rowling", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fantasy novel", false, "Text 9", "Harry Potter and the Sorcerer's Stone", null },
                    { 10L, null, "J.R.R. Tolkien", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fantasy adventure", false, "Text 10", "The Hobbit", null },
                    { 11L, null, "Charles Dickens", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roman klassikasi", false, "Matni 11", "Oliver Twist", null },
                    { 12L, null, "Arthyr Konan Doil", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mashhur detektiv qissalari", false, "Matni 12", "Sherlok Holms", null },
                    { 13L, null, "Fyodor Dostoyevski", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rus roman klassikasi", false, "Matni 13", "Qo'shiqchi", null },
                    { 14L, null, "H. P. Lovecraft", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fantastika", false, "Matni 14", "Zulumotlar va qo'rqinlar vaqti", null },
                    { 15L, null, "Stephen King", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "G'azablandiruvchi roman", false, "Matni 15", "Qorquv", null },
                    { 16L, null, "Herman Melville", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Qayiq ko'prik qirg'ishi", false, "Matni 16", "Mobi-Dik", null },
                    { 17L, null, "Munis Xo'ja", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xalq qahramoni tarixiy roman", false, "Matni 17", "Andijonlik", null },
                    { 18L, null, "Alexander Duma", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maktab roman klassikasi", false, "Matni 18", "Sulton Kuzo", null },
                    { 19L, null, "Edgar Allan Poe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "G'azallar", false, "Matni 19", "Qo'rqinchli g'azal", null },
                    { 20L, null, "Ivan Denisovich", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Olam shekillari roman", false, "Matni 20", "Bir kunda", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AssetId", "CreatedAt", "Email", "FirstName", "IsDeleted", "LastName", "PasswordHash", "Phone", "UpdatedAt", "UserRole" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imona.kabirova@example.com", "Imona", false, "Kabirova", "a", "9001234567", null, 1 },
                    { 2L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jamshid.zayniev@example.com", "Jamshid", false, "Zayniev", "a", "9007654321", null, 1 },
                    { 3L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "anastasiya.tomchuk@example.com", "Anastasiya", false, "Tomchuk", "a", "9009876543", null, 1 },
                    { 4L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "iskandar.kodirov@example.com", "Iskandar", false, "Kodirov", "a", "9012345678", null, 1 },
                    { 5L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nodirshax.allanazarov@example.com", "Nodirshax", false, "Allanazarov", "a", "9012345679", null, 1 },
                    { 7L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "asilbek.abdurashidov@example.com", "Asilbek", false, "Abdurashidov", "a", "9012345679", null, 1 },
                    { 8L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jasurbek.ergashev@example.com", "Jasurbek", false, "Ergashev", "a", "9012345680", null, 1 },
                    { 9L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "takhmina.saidova@example.com", "Takhmina", false, "Saidova", "a", "9012345681", null, 1 },
                    { 10L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "asadbek.qarshiyev@example.com", "Asadbek", false, "Qarshiyev", "a", "9012345682", null, 1 },
                    { 11L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sardor.sohinazarov@example.com", "Sardor", false, "Sohinazarov", "a", "9012345683", null, 1 },
                    { 12L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "raykhona.isroilova@example.com", "Raykhona", false, "Isroilova", "a", "9012345684", null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12L);
        }
    }
}
