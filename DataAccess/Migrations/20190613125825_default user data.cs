using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class defaultuserdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedAt", "RoleId", "Username" },
                values: new object[] { 3, "php1.store.test@gmail.com", "Monty", false, "Python", null, 1, "superadmin" });

            migrationBuilder.InsertData(
                table: "Passwords",
                columns: new[] { "Id", "Hidden", "IsDeleted", "ModifiedAt", "UserId" },
                values: new object[] { 1001, "Pwd1234!", false, null, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Passwords",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
