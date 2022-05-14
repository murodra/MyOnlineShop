using Microsoft.EntityFrameworkCore.Migrations;

namespace MyOnlineShop.Data.Migrations
{
    public partial class categoryentityupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryParentId",
                table: "Categorys",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categorys_CategoryParentId",
                table: "Categorys",
                column: "CategoryParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorys_Categorys_CategoryParentId",
                table: "Categorys",
                column: "CategoryParentId",
                principalTable: "Categorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorys_Categorys_CategoryParentId",
                table: "Categorys");

            migrationBuilder.DropIndex(
                name: "IX_Categorys_CategoryParentId",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "CategoryParentId",
                table: "Categorys");
        }
    }
}
