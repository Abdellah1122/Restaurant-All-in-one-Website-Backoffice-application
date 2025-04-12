using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class JGo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Tables_TableId",
                table: "Commandes");

            migrationBuilder.DropIndex(
                name: "IX_Commandes_TableId",
                table: "Commandes");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "Commandes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TableId",
                table: "Commandes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_TableId",
                table: "Commandes",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Tables_TableId",
                table: "Commandes",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
