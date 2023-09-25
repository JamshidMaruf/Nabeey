using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nabeey.DataAccess.Migrations
{
    public partial class AddedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
