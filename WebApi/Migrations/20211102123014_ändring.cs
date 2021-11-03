using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class ändring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categoris_CategoryId",
                table: "SubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categoris",
                table: "Categoris");

            migrationBuilder.RenameTable(
                name: "Categoris",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Categoris_Name",
                table: "Categories",
                newName: "IX_Categories_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categoris");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Name",
                table: "Categoris",
                newName: "IX_Categoris_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categoris",
                table: "Categoris",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categoris_CategoryId",
                table: "SubCategories",
                column: "CategoryId",
                principalTable: "Categoris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
