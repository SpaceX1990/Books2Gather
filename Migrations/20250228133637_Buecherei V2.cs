using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Books2Gather.Migrations
{
    /// <inheritdoc />
    public partial class BuechereiV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autoren_Books_BookId",
                table: "Autoren");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Autoren",
                table: "Autoren");

            migrationBuilder.RenameTable(
                name: "Autoren",
                newName: "Authors");

            migrationBuilder.RenameIndex(
                name: "IX_Autoren_BookId",
                table: "Authors",
                newName: "IX_Authors_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Books_BookId",
                table: "Authors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Books_BookId",
                table: "Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Autoren");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_BookId",
                table: "Autoren",
                newName: "IX_Autoren_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Autoren",
                table: "Autoren",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Autoren_Books_BookId",
                table: "Autoren",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId");
        }
    }
}
