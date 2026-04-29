using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddPrintingHouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrintingHouseId",
                table: "Books",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PrintingHouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Website = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintingHouses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_PrintingHouseId",
                table: "Books",
                column: "PrintingHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_PrintingHouses_Name",
                table: "PrintingHouses",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_PrintingHouses_PrintingHouseId",
                table: "Books",
                column: "PrintingHouseId",
                principalTable: "PrintingHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_PrintingHouses_PrintingHouseId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "PrintingHouses");

            migrationBuilder.DropIndex(
                name: "IX_Books_PrintingHouseId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PrintingHouseId",
                table: "Books");
        }
    }
}
