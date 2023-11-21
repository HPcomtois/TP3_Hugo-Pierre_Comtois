using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP3.Migrations
{
    /// <inheritdoc />
    public partial class seed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "916ba4c0-5bca-41d1-97d0-211a13881617", "AQAAAAIAAYagAAAAEOIf8QyLkfjAeeCTw03e4JALbHM9yhLbkUM5QU14iEraEwiwJnj/6Lfu0YA73Ka+ww==", "91cd9591-2ce4-4fc7-bfc3-865489b7707a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9eed3ec8-5a12-4c06-b175-09075ff488e6", "AQAAAAIAAYagAAAAEKuQxJSNmhxg+3pZdHuStVtyjmLl0h1SSZIuczxOhiI9GNyVWKVJe9DWphTC/hFUMw==", "c8463b9a-7dfd-40e0-82d7-27ed9c2cbaa1" });
        }
    }
}
