using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nasa.DAL.Migrations
{
    public partial class removeuselesstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Settlements_SettlementId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_SettlementId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                table: "Subscriptions");

            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "SettlementId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Сoordinates = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SettlementId",
                table: "Subscriptions",
                column: "SettlementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Settlements_SettlementId",
                table: "Subscriptions",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
