using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Data.Migrations
{
    public partial class droptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "User_Name",
                table: "Like",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_Name",
                table: "Like");

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
                name: "IX_UserName_LikeID",
                table: "UserName",
                column: "LikeID");
        }
    }
}
