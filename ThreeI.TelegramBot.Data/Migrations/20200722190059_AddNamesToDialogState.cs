using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeI.TelegramBot.Data.Migrations
{
    public partial class AddNamesToDialogState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "dialog_states",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "dialog_states",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                table: "dialog_states");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "dialog_states");
        }
    }
}
