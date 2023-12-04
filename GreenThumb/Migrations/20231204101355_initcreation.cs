using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenThumb.Migrations
{
    public partial class initcreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Instruction",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    plant_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruction", x => x.id);
                    table.ForeignKey(
                        name: "FK_Instruction_Plant_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Garden",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    size = table.Column<int>(type: "int", nullable: true),
                    level = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garden", x => x.id);
                    table.ForeignKey(
                        name: "FK_Garden_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GardenPlant",
                columns: table => new
                {
                    GardensGardenId = table.Column<int>(type: "int", nullable: false),
                    PlantsPlantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GardenPlant", x => new { x.GardensGardenId, x.PlantsPlantId });
                    table.ForeignKey(
                        name: "FK_GardenPlant_Garden_GardensGardenId",
                        column: x => x.GardensGardenId,
                        principalTable: "Garden",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GardenPlant_Plant_PlantsPlantId",
                        column: x => x.PlantsPlantId,
                        principalTable: "Plant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Plant",
                columns: new[] { "id", "name" },
                values: new object[] { 1, "flower" });

            migrationBuilder.InsertData(
                table: "Plant",
                columns: new[] { "id", "name" },
                values: new object[] { 2, "strawberry" });

            migrationBuilder.InsertData(
                table: "Plant",
                columns: new[] { "id", "name" },
                values: new object[] { 3, "cactus" });

            migrationBuilder.InsertData(
                table: "Instruction",
                columns: new[] { "id", "description", "plant_id" },
                values: new object[] { 1, "water the plant", 1 });

            migrationBuilder.InsertData(
                table: "Instruction",
                columns: new[] { "id", "description", "plant_id" },
                values: new object[] { 2, "harvest the plant", 2 });

            migrationBuilder.InsertData(
                table: "Instruction",
                columns: new[] { "id", "description", "plant_id" },
                values: new object[] { 3, "fertilize the plant", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Garden_user_id",
                table: "Garden",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GardenPlant_PlantsPlantId",
                table: "GardenPlant",
                column: "PlantsPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_plant_id",
                table: "Instruction",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_name",
                table: "Plant",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_name",
                table: "User",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GardenPlant");

            migrationBuilder.DropTable(
                name: "Instruction");

            migrationBuilder.DropTable(
                name: "Garden");

            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
