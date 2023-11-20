using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP3.Migrations
{
    /// <inheritdoc />
    public partial class newList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voyage_AspNetUsers_UserId",
                table: "Voyage");

            migrationBuilder.DropIndex(
                name: "IX_Voyage_UserId",
                table: "Voyage");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Voyage");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Voyage",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserVoyage",
                columns: table => new
                {
                    UsersId = table.Column<string>(type: "TEXT", nullable: false),
                    VoyagesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVoyage", x => new { x.UsersId, x.VoyagesId });
                    table.ForeignKey(
                        name: "FK_UserVoyage_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVoyage_Voyage_VoyagesId",
                        column: x => x.VoyagesId,
                        principalTable: "Voyage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVoyage_VoyagesId",
                table: "UserVoyage",
                column: "VoyagesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVoyage");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Voyage",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Voyage",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voyage_UserId",
                table: "Voyage",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voyage_AspNetUsers_UserId",
                table: "Voyage",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
