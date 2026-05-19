using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class issue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDoorClosed",
                table: "Lockers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "IssueCompartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    InsertionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PinCodeLibrarian = table.Column<string>(type: "TEXT", nullable: true),
                    PinCodeReader = table.Column<string>(type: "TEXT", nullable: true),
                    LockerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueCompartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueCompartments_Lockers_LockerId",
                        column: x => x.LockerId,
                        principalTable: "Lockers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueCompartments_LockerId",
                table: "IssueCompartments",
                column: "LockerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueCompartments");

            migrationBuilder.DropColumn(
                name: "IsDoorClosed",
                table: "Lockers");
        }
    }
}
