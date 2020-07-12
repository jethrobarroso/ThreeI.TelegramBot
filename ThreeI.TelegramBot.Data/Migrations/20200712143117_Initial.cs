using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ThreeI.TelegramBot.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "supervisors",
                columns: table => new
                {
                    supervisor_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    full_name = table.Column<string>(maxLength: 50, nullable: false),
                    chat_id = table.Column<long>(nullable: false),
                    telegram_user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supervisors", x => x.supervisor_id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    category_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(maxLength: 25, nullable: false),
                    supervisor_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.category_id);
                    table.ForeignKey(
                        name: "FK_categories_supervisors_supervisor_id",
                        column: x => x.supervisor_id,
                        principalTable: "supervisors",
                        principalColumn: "supervisor_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dialog_states",
                columns: table => new
                {
                    dialog_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user_id = table.Column<string>(nullable: true),
                    block = table.Column<string>(nullable: true),
                    unit = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    confirmation = table.Column<int>(nullable: false),
                    chat_phase = table.Column<int>(nullable: false),
                    last_active = table.Column<DateTime>(nullable: false),
                    is_support_mode = table.Column<bool>(nullable: false),
                    category_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dialog_states", x => x.dialog_id);
                    table.ForeignKey(
                        name: "FK_dialog_states_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    report_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    first_name = table.Column<string>(maxLength: 50, nullable: true),
                    last_name = table.Column<string>(maxLength: 50, nullable: true),
                    block = table.Column<string>(maxLength: 20, nullable: false),
                    unit = table.Column<string>(nullable: false),
                    description = table.Column<string>(maxLength: 500, nullable: false),
                    date_logged = table.Column<DateTime>(nullable: false),
                    DialogStateId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.report_id);
                    table.ForeignKey(
                        name: "FK_reports_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_reports_dialog_states_DialogStateId",
                        column: x => x.DialogStateId,
                        principalTable: "dialog_states",
                        principalColumn: "dialog_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categories_supervisor_id",
                table: "categories",
                column: "supervisor_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dialog_states_category_id",
                table: "dialog_states",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_reports_CategoryId",
                table: "reports",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_reports_DialogStateId",
                table: "reports",
                column: "DialogStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "dialog_states");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "supervisors");
        }
    }
}
