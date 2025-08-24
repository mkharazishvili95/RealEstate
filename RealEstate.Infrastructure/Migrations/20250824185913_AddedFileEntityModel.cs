using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    public partial class AddedFileEntityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentDealType",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApartmentState",
                table: "Apartments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApartmentType",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuildingStatus",
                table: "Apartments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Apartments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Apartments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StreetId",
                table: "Apartments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubdistrictId",
                table: "Apartments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentId = table.Column<int>(type: "int", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaidService = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.DropColumn(
                name: "ApartmentDealType",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "ApartmentState",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "ApartmentType",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "BuildingStatus",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "StreetId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "SubdistrictId",
                table: "Apartments");
        }
    }
}
