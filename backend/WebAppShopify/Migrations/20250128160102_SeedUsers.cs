using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppShopify.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "PasswordHash", "Role", "Username" },
                values: new object[] { 1, "$2a$11$F7BeXkVAaNobjPVBBg8/JObJ3SEKbp.o/ozyRNa3UXsILMqCnucWO", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "PasswordHash", "Role", "Username" },
                values: new object[] { 2, "$2a$11$LaN/GlIZlPrnaI8Q3iUHGe4KCIrNxbTAkRgW6sfLHBrh5ZtMTa2pW", "Viewer", "viewer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);
        }
    }
}
