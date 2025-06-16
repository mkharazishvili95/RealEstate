using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    public partial class AgencyIntoApartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Apartments_AgencyId",
                table: "Apartments",
                column: "AgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Agencies_AgencyId",
                table: "Apartments",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "AgencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Agencies_AgencyId",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_AgencyId",
                table: "Apartments");
        }
    }
}
