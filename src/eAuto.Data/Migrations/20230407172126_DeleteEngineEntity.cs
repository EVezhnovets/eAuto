using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAuto.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteEngineEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_BodyTypes_BodyTypeId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_DriveTypes_DriveTypeId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Engines_EngineId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_GenerationDataModel_GenerationId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Transmissions_TransmissionId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_GenerationDataModel_Brands_BrandId",
                table: "GenerationDataModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GenerationDataModel_Models_ModelId",
                table: "GenerationDataModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DropIndex(
                name: "IX_Cars_EngineId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "EngineId",
                table: "Cars",
                newName: "EnginePower");

            migrationBuilder.AddColumn<int>(
                name: "EngineCapacity",
                table: "Cars",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EngineFuelType",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EngineIdentificationName",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_BodyTypes_BodyTypeId",
                table: "Cars",
                column: "BodyTypeId",
                principalTable: "BodyTypes",
                principalColumn: "BodyTypeId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_DriveTypes_DriveTypeId",
                table: "Cars",
                column: "DriveTypeId",
                principalTable: "DriveTypes",
                principalColumn: "DriveTypeId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_GenerationDataModel_GenerationId",
                table: "Cars",
                column: "GenerationId",
                principalTable: "GenerationDataModel",
                principalColumn: "GenerationId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Transmissions_TransmissionId",
                table: "Cars",
                column: "TransmissionId",
                principalTable: "Transmissions",
                principalColumn: "TransmissionId",
                onDelete: ReferentialAction.NoAction);
                
            migrationBuilder.AddForeignKey(
                name: "FK_GenerationDataModel_Brands_BrandId",
                table: "GenerationDataModel",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GenerationDataModel_Models_ModelId",
                table: "GenerationDataModel",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_BodyTypes_BodyTypeId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_DriveTypes_DriveTypeId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_GenerationDataModel_GenerationId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Transmissions_TransmissionId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_GenerationDataModel_Brands_BrandId",
                table: "GenerationDataModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GenerationDataModel_Models_ModelId",
                table: "GenerationDataModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "EngineCapacity",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EngineFuelType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EngineIdentificationName",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "EnginePower",
                table: "Cars",
                newName: "EngineId");

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    EngineId = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    GenerationId = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    ModelId = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Capacity = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdentificationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Power = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.EngineId);
                    table.ForeignKey(
                        name: "FK_Engines_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId");
                    table.ForeignKey(
                        name: "FK_Engines_GenerationDataModel_GenerationId",
                        column: x => x.GenerationId,
                        principalTable: "GenerationDataModel",
                        principalColumn: "GenerationId");
                    table.ForeignKey(
                        name: "FK_Engines_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "ModelId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_EngineId",
                table: "Cars",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_BrandId",
                table: "Engines",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_GenerationId",
                table: "Engines",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_ModelId",
                table: "Engines",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_BodyTypes_BodyTypeId",
                table: "Cars",
                column: "BodyTypeId",
                principalTable: "BodyTypes",
                principalColumn: "BodyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_DriveTypes_DriveTypeId",
                table: "Cars",
                column: "DriveTypeId",
                principalTable: "DriveTypes",
                principalColumn: "DriveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Engines_EngineId",
                table: "Cars",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "EngineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_GenerationDataModel_GenerationId",
                table: "Cars",
                column: "GenerationId",
                principalTable: "GenerationDataModel",
                principalColumn: "GenerationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Transmissions_TransmissionId",
                table: "Cars",
                column: "TransmissionId",
                principalTable: "Transmissions",
                principalColumn: "TransmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenerationDataModel_Brands_BrandId",
                table: "GenerationDataModel",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenerationDataModel_Models_ModelId",
                table: "GenerationDataModel",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId");
        }
    }
}
