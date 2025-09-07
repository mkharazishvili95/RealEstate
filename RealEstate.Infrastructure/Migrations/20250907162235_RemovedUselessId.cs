using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    public partial class RemovedUselessId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserFavoriteApplicationId",
                table: "UserFavoriteApartments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserFavoriteApplicationId",
                table: "UserFavoriteApartments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
