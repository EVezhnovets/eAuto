using Microsoft.EntityFrameworkCore.Migrations;

namespace eAuto.Data.Migrations
{
    public partial class AddMotorOilAndProductBrandsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductBrands",
                columns: table => new
                {
                    ProductBrandDataModelId = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrands", x => x.ProductBrandDataModelId);
                });

            migrationBuilder.CreateTable(
                name: "MotorOils",
                columns: table => new
                {
                    MotorOilDataModelId = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Viscosity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Composition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Volume = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    ProductBrandDataModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorOils", x => x.MotorOilDataModelId);
                    table.ForeignKey(
                        name: "FK_MotorOils_ProductBrands_ProductBrandDataModelId",
                        column: x => x.ProductBrandDataModelId,
                        principalTable: "ProductBrands",
                        principalColumn: "ProductBrandDataModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MotorOils_ProductBrandDataModelId",
                table: "MotorOils",
                column: "ProductBrandDataModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotorOils");

            migrationBuilder.DropTable(
                name: "ProductBrands");
        }
    }
}