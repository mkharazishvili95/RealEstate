using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    public partial class AddedBlockReasonToApartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlockReason",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Agencies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_UserId",
                table: "Agencies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agencies_Users_UserId",
                table: "Agencies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencies_Users_UserId",
                table: "Agencies");

            migrationBuilder.DropIndex(
                name: "IX_Agencies_UserId",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "BlockReason",
                table: "Apartments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Agencies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
