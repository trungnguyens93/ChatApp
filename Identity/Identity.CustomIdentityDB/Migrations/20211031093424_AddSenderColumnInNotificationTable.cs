using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.CustomIdentityDB.Migrations
{
    public partial class AddSenderColumnInNotificationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sender",
                table: "Notifications");
        }
    }
}
