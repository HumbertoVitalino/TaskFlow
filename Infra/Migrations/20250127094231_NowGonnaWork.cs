using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class NowGonnaWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskTags");

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TagId",
                table: "Tasks",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tags_TagId",
                table: "Tasks",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tags_TagId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TagId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Tasks");

            migrationBuilder.CreateTable(
                name: "TaskTags",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTags", x => new { x.TaskId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TaskTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTags_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskTags_TagId",
                table: "TaskTags",
                column: "TagId");
        }
    }
}
