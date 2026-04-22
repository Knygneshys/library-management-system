using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddPrintingHouseTable : Migration
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

            migrationBuilder.CreateIndex(
                name: "IX_Books_PrintingHouseId",
                table: "Books",
                column: "PrintingHouseId");

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

            migrationBuilder.DropIndex(
                name: "IX_Books_PrintingHouseId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PrintingHouseId",
                table: "Books");
        }
    }
}
