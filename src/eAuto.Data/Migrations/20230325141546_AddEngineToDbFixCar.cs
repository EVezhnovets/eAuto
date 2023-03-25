using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAuto.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEngineToDbFixCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "BodyType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DriveType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EngineType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Generation",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "EngineTypes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "EngineCapacity",
                table: "Cars",
                newName: "TransmissionId");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "EngineTypes",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "EngineTypes",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "EngineTypes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GenerationId",
                table: "EngineTypes",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IdentificationName",
                table: "EngineTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "EngineTypes",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "EngineTypes",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BodyTypeId",
                table: "Cars",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Cars",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DriveTypeId",
                table: "Cars",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EngineId",
                table: "Cars",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenerationId",
                table: "Cars",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "Cars",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EngineTypes_BrandId",
                table: "EngineTypes",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_EngineTypes_GenerationId",
                table: "EngineTypes",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_EngineTypes_ModelId",
                table: "EngineTypes",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BodyTypeId",
                table: "Cars",
                column: "BodyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandId",
                table: "Cars",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DriveTypeId",
                table: "Cars",
                column: "DriveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_EngineId",
                table: "Cars",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_GenerationId",
                table: "Cars",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ModelId",
                table: "Cars",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_TransmissionId",
                table: "Cars",
                column: "TransmissionId");

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
                name: "FK_Cars_EngineTypes_EngineId",
                table: "Cars",
                column: "EngineId",
                principalTable: "EngineTypes",
                principalColumn: "EngineTypeId",
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
                name: "FK_EngineTypes_Brands_BrandId",
                table: "EngineTypes",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EngineTypes_GenerationDataModel_GenerationId",
                table: "EngineTypes",
                column: "GenerationId",
                principalTable: "GenerationDataModel",
                principalColumn: "GenerationId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EngineTypes_Models_ModelId",
                table: "EngineTypes",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "ModelId",
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
                name: "FK_Cars_EngineTypes_EngineId",
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
                name: "FK_EngineTypes_Brands_BrandId",
                table: "EngineTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_EngineTypes_GenerationDataModel_GenerationId",
                table: "EngineTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_EngineTypes_Models_ModelId",
                table: "EngineTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_GenerationDataModel_Brands_BrandId",
                table: "GenerationDataModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GenerationDataModel_Models_ModelId",
                table: "GenerationDataModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_EngineTypes_BrandId",
                table: "EngineTypes");

            migrationBuilder.DropIndex(
                name: "IX_EngineTypes_GenerationId",
                table: "EngineTypes");

            migrationBuilder.DropIndex(
                name: "IX_EngineTypes_ModelId",
                table: "EngineTypes");

            migrationBuilder.DropIndex(
                name: "IX_Cars_BodyTypeId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_BrandId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_DriveTypeId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_EngineId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_GenerationId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ModelId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_TransmissionId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "EngineTypes");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "EngineTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "EngineTypes");

            migrationBuilder.DropColumn(
                name: "GenerationId",
                table: "EngineTypes");

            migrationBuilder.DropColumn(
                name: "IdentificationName",
                table: "EngineTypes");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "EngineTypes");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "EngineTypes");

            migrationBuilder.DropColumn(
                name: "BodyTypeId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DriveTypeId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EngineId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "GenerationId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "EngineTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TransmissionId",
                table: "Cars",
                newName: "EngineCapacity");

            migrationBuilder.AddColumn<string>(
                name: "BodyType",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriveType",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EngineType",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Generation",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Transmission",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

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
