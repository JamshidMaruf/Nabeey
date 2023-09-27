using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nabeey.DataAccess.Migrations
{
    public partial class AddSomeTablesFluentApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Questions_ImageId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_AssetId",
                table: "Answers");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ImageId",
                table: "Questions",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AssetId",
                table: "Answers",
                column: "AssetId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Questions_ImageId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_AssetId",
                table: "Answers");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ImageId",
                table: "Questions",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AssetId",
                table: "Answers",
                column: "AssetId");
        }
    }
}
