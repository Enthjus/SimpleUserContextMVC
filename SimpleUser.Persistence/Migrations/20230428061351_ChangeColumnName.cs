using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleUser.Persistence.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Customer",
                newName: "Customername");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Customername",
                table: "Customer",
                newName: "Username");
        }
    }
}
