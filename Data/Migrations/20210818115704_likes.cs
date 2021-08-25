using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Data.Migrations
{
    public partial class likes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    LikeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.LikeID);
                    table.ForeignKey(
                        name: "FK_Like_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserName",
                columns: table => new
                {
                    User_Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserName", x => x.User_Name);
                    table.ForeignKey(
                        name: "FK_UserName_Like_LikeID",
                        column: x => x.LikeID,
                        principalTable: "Like",
                        principalColumn: "LikeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_ItemID",
                table: "Like",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_UserName_LikeID",
                table: "UserName",
                column: "LikeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserName");

            migrationBuilder.DropTable(
                name: "Like");
        }
    }
}
