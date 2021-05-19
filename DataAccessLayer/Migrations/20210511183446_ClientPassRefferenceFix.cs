using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class ClientPassRefferenceFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Entries__PassId__2739D489",
                table: "Entries");

            migrationBuilder.RenameColumn(
                name: "PassId",
                table: "Entries",
                newName: "ClientPassId");

            migrationBuilder.RenameIndex(
                name: "IX_Entries_PassId",
                table: "Entries",
                newName: "IX_Entries_ClientPassId");

            migrationBuilder.AddForeignKey(
                name: "FK__Entries__PassId__2739D489",
                table: "Entries",
                column: "ClientPassId",
                principalTable: "ClientPasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Entries__PassId__2739D489",
                table: "Entries");

            migrationBuilder.RenameColumn(
                name: "ClientPassId",
                table: "Entries",
                newName: "PassId");

            migrationBuilder.RenameIndex(
                name: "IX_Entries_ClientPassId",
                table: "Entries",
                newName: "IX_Entries_PassId");

            migrationBuilder.AddForeignKey(
                name: "FK__Entries__PassId__2739D489",
                table: "Entries",
                column: "PassId",
                principalTable: "Passes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
