using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Data.Migrations
{
    public partial class updt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Comment_ItemID",
                table: "Comment",
                column: "ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Item_ItemID",
                table: "Comment",
                column: "ItemID",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Item_ItemID",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ItemID",
                table: "Comment");
        }
    }
}
