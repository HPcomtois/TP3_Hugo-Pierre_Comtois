using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP3.Migrations
{
    /// <inheritdoc />
    public partial class Ajout_photo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3c637f5-e41d-480f-8dc9-c553fd9aed85", "AQAAAAIAAYagAAAAEDQZf2u6w35k2G0W0Ar2QZ2HUBoQw2B54DJTGnFGGv588mKXbbSbQYE5/Zz7cwEEVQ==", "83487b26-1d87-4584-9e27-d10fed43e2b3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "951ea609-fc50-410f-9701-126e3e0fa61d", "AQAAAAIAAYagAAAAEOcMFjoNxmr5zNl1mqd4uRBDeBOFGSFn1nT5mNXJSjgkWeqAIOr+ecclhoN19AHqZw==", "c0b006d6-d688-4e4b-9bc0-90e39980c2f5" });
        }
    }
}
