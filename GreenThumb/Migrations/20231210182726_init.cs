using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenThumb.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    plant_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Instructions_Plants_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gardens",
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
                    table.PrimaryKey("PK_Gardens", x => x.id);
                    table.ForeignKey(
                        name: "FK_Gardens_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
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
                        name: "FK_GardenPlant_Gardens_GardensGardenId",
                        column: x => x.GardensGardenId,
                        principalTable: "Gardens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GardenPlant_Plants_PlantsPlantId",
                        column: x => x.PlantsPlantId,
                        principalTable: "Plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "id", "name" },
                values: new object[] { 1, "flower" });

            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "id", "name" },
                values: new object[] { 2, "strawberry" });

            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "id", "name" },
                values: new object[] { 3, "cactus" });

            migrationBuilder.InsertData(
                table: "Instructions",
                columns: new[] { "id", "description", "plant_id" },
                values: new object[,]
                {
                    { 1, "water the plant", 1 },
                    { 2, "fertilize the plant", 1 },
                    { 3, "harvest the plant", 1 },
                    { 4, "water the plant", 2 },
                    { 5, "fertilize the plant", 2 },
                    { 6, "harvest the plant", 2 },
                    { 7, "water the plant", 3 },
                    { 8, "fertilize the plant", 3 },
                    { 9, "harvest the plant", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GardenPlant_PlantsPlantId",
                table: "GardenPlant",
                column: "PlantsPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Gardens_user_id",
                table: "Gardens",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_plant_id",
                table: "Instructions",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_name",
                table: "Plants",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_name",
                table: "Users",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GardenPlant");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "Gardens");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
