using Microsoft.EntityFrameworkCore.Migrations;

namespace eAuto.Data.Migrations
{
    public partial class AddNgineTypeIdToCarToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EngineFuelTypeId",
                table: "Cars",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngineFuelTypeId",
                table: "Cars");
        }
    }
}