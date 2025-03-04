using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildMasterPro.Migrations
{
    /// <inheritdoc />
    public partial class ClientEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternatePersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternatePersonEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternatePersonPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEstablished = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Project");
        }
    }
}
