using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAuto.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixPriceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "MotorOils",
                type: "float",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "MotorOils",
                type: "decimal(18,2)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 50);
        }
    }
}
