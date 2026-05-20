using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Books_BooksId",
                table: "BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Genres_GenresId",
                table: "BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_IssueCompartments_IssueCompartmentId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_IssueCompartments_ReturnCompartmentId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "LibraryTasks");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CopyId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_IssueCompartmentId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReturnCompartmentId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_IssueCompartments_LockerId",
                table: "IssueCompartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookGenre",
                table: "BookGenre");

            migrationBuilder.DropColumn(
                name: "IssueCompartmentId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReturnCompartmentId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IssueCompartmentId",
                table: "Lockers");

            migrationBuilder.RenameTable(
                name: "BookGenre",
                newName: "BookGenres");

            migrationBuilder.RenameColumn(
                name: "InsertionDate",
                table: "IssueCompartments",
                newName: "ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_BookGenre_GenresId",
                table: "BookGenres",
                newName: "IX_BookGenres_GenresId");

            migrationBuilder.AlterColumn<string>(
                name: "LocationCode",
                table: "Lockers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PinCodeReader",
                table: "IssueCompartments",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PinCodeLibrarian",
                table: "IssueCompartments",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "IssueCompartments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookGenres",
                table: "BookGenres",
                columns: new[] { "BooksId", "GenresId" });

            migrationBuilder.CreateTable(
                name: "LibrarianTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsIssueTask = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReservationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibrarianTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibrarianTasks_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CopyId",
                table: "Reservations",
                column: "CopyId");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Name",
                table: "Publishers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssueCompartments_LockerId",
                table: "IssueCompartments",
                column: "LockerId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueCompartments_ReservationId",
                table: "IssueCompartments",
                column: "ReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Title",
                table: "Genres",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibrarianTasks_ReservationId",
                table: "LibrarianTasks",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Books_BooksId",
                table: "BookGenres",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Genres_GenresId",
                table: "BookGenres",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueCompartments_Reservations_ReservationId",
                table: "IssueCompartments",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Books_BooksId",
                table: "BookGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Genres_GenresId",
                table: "BookGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueCompartments_Reservations_ReservationId",
                table: "IssueCompartments");

            migrationBuilder.DropTable(
                name: "LibrarianTasks");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CopyId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_Name",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_IssueCompartments_LockerId",
                table: "IssueCompartments");

            migrationBuilder.DropIndex(
                name: "IX_IssueCompartments_ReservationId",
                table: "IssueCompartments");

            migrationBuilder.DropIndex(
                name: "IX_Genres_Title",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookGenres",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "IssueCompartments");

            migrationBuilder.RenameTable(
                name: "BookGenres",
                newName: "BookGenre");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "IssueCompartments",
                newName: "InsertionDate");

            migrationBuilder.RenameIndex(
                name: "IX_BookGenres_GenresId",
                table: "BookGenre",
                newName: "IX_BookGenre_GenresId");

            migrationBuilder.AddColumn<Guid>(
                name: "IssueCompartmentId",
                table: "Reservations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReturnCompartmentId",
                table: "Reservations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LocationCode",
                table: "Lockers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "IssueCompartmentId",
                table: "Lockers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "PinCodeReader",
                table: "IssueCompartments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PinCodeLibrarian",
                table: "IssueCompartments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookGenre",
                table: "BookGenre",
                columns: new[] { "BooksId", "GenresId" });

            migrationBuilder.CreateTable(
                name: "LibraryTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReservationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsIssueTask = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryTasks_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CopyId",
                table: "Reservations",
                column: "CopyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IssueCompartmentId",
                table: "Reservations",
                column: "IssueCompartmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReturnCompartmentId",
                table: "Reservations",
                column: "ReturnCompartmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssueCompartments_LockerId",
                table: "IssueCompartments",
                column: "LockerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibraryTasks_ReservationId",
                table: "LibraryTasks",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Books_BooksId",
                table: "BookGenre",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Genres_GenresId",
                table: "BookGenre",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_IssueCompartments_IssueCompartmentId",
                table: "Reservations",
                column: "IssueCompartmentId",
                principalTable: "IssueCompartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_IssueCompartments_ReturnCompartmentId",
                table: "Reservations",
                column: "ReturnCompartmentId",
                principalTable: "IssueCompartments",
                principalColumn: "Id");
        }
    }
}
