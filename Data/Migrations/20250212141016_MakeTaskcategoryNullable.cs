using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildMasterPro.Migrations
{
    /// <inheritdoc />
    public partial class MakeTaskcategoryNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTask_TaskCategory_CategoryId",
                table: "ProjectTask");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ProjectTask",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTask_TaskCategory_CategoryId",
                table: "ProjectTask",
                column: "CategoryId",
                principalTable: "TaskCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTask_TaskCategory_CategoryId",
                table: "ProjectTask");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ProjectTask",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTask_TaskCategory_CategoryId",
                table: "ProjectTask",
                column: "CategoryId",
                principalTable: "TaskCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
