using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Data.Migrations
{
    public partial class updatemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "User_Name",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Collection",
                newName: "CollectionID");

            migrationBuilder.AddColumn<int>(
                name: "CollectionID",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Item_CollectionID",
                table: "Item",
                column: "CollectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Collection_CollectionID",
                table: "Item",
                column: "CollectionID",
                principalTable: "Collection",
                principalColumn: "CollectionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Collection_CollectionID",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_CollectionID",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "CollectionID",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "CollectionID",
                table: "Collection",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "CollId",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_Name",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
