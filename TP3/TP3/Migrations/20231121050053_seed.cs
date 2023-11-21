using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP3.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "11111111-1111-1111-1111-111111111111", 0, "9eed3ec8-5a12-4c06-b175-09075ff488e6", "candyCruise@mail.com", false, false, null, "CANDYCRUISE@MAIL.COM", "BIGBOY32", "AQAAAAIAAYagAAAAEKuQxJSNmhxg+3pZdHuStVtyjmLl0h1SSZIuczxOhiI9GNyVWKVJe9DWphTC/hFUMw==", null, false, "c8463b9a-7dfd-40e0-82d7-27ed9c2cbaa1", false, "Bigboy32" });

            migrationBuilder.InsertData(
                table: "Voyage",
                columns: new[] { "Id", "Img", "Name", "Visible" },
                values: new object[,]
                {
                    { 1, "https://www.routesdumonde.com/wp-content/uploads/2019/06/Thumbnail-Japon.jpg", "Allemagne", true },
                    { 2, "https://www.routesdumonde.com/wp-content/uploads/2019/06/Thumbnail-Japon.jpg", "Algérie", false }
                });

            migrationBuilder.InsertData(
                table: "UserVoyage",
                columns: new[] { "UsersId", "VoyagesId" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", 1 },
                    { "11111111-1111-1111-1111-111111111111", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserVoyage",
                keyColumns: new[] { "UsersId", "VoyagesId" },
                keyValues: new object[] { "11111111-1111-1111-1111-111111111111", 1 });

            migrationBuilder.DeleteData(
                table: "UserVoyage",
                keyColumns: new[] { "UsersId", "VoyagesId" },
                keyValues: new object[] { "11111111-1111-1111-1111-111111111111", 2 });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.DeleteData(
                table: "Voyage",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Voyage",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
