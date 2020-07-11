using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeI.TelegramBot.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dialog_states",
                columns: table => new
                {
                    dialog_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user_id = table.Column<string>(nullable: true),
                    block = table.Column<string>(nullable: true),
                    unit = table.Column<string>(nullable: true),
                    category = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    confirmation = table.Column<int>(nullable: false),
                    chat_phase = table.Column<int>(nullable: false),
                    last_active = table.Column<DateTime>(nullable: false),
                    is_support_mode = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dialog_states", x => x.dialog_id);
                });

            migrationBuilder.CreateTable(
                name: "fault_reports",
                columns: table => new
                {
                    report_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    dialog_id = table.Column<int>(nullable: true),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    block = table.Column<string>(nullable: true),
                    unit = table.Column<string>(nullable: true),
                    category = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_logged = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fault_reports", x => x.report_id);
                    table.ForeignKey(
                        name: "FK_fault_reports_dialog_states_dialog_id",
                        column: x => x.dialog_id,
                        principalTable: "dialog_states",
                        principalColumn: "dialog_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fault_reports_dialog_id",
                table: "fault_reports",
                column: "dialog_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fault_reports");

            migrationBuilder.DropTable(
                name: "dialog_states");
        }
    }
}
