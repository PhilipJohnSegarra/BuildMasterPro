using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildMasterPro.Migrations
{
    /// <inheritdoc />
    public partial class NewTaskCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "TaskCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TaskCategories_ProjectId",
                table: "TaskCategories",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCategories_Project_ProjectId",
                table: "TaskCategories",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskCategories_Project_ProjectId",
                table: "TaskCategories");

            migrationBuilder.DropIndex(
                name: "IX_TaskCategories_ProjectId",
                table: "TaskCategories");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "TaskCategories");
        }
    }
}
