using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class MinorFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstUsedAt",
                table: "ClientPasses");
            migrationBuilder.AddColumn<DateTime>(
                name: "FirstUsedAt",
                table: "ClientPasses",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passes_ValidForGymId",
                table: "Passes",
                column: "ValidForGymId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passes_Gyms_ValidForGymId",
                table: "Passes",
                column: "ValidForGymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passes_Gyms_ValidForGymId",
                table: "Passes");

            migrationBuilder.DropIndex(
                name: "IX_Passes_ValidForGymId",
                table: "Passes");

            migrationBuilder.AlterColumn<int>(
                name: "FirstUsedAt",
                table: "ClientPasses",
                type: "int",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
