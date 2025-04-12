using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class JGq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Tables_tableId",
                table: "Commandes");

            migrationBuilder.RenameColumn(
                name: "tableId",
                table: "Commandes",
                newName: "TableId");

            migrationBuilder.RenameIndex(
                name: "IX_Commandes_tableId",
                table: "Commandes",
                newName: "IX_Commandes_TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Tables_TableId",
                table: "Commandes",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Tables_TableId",
                table: "Commandes");

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "Commandes",
                newName: "tableId");

            migrationBuilder.RenameIndex(
                name: "IX_Commandes_TableId",
                table: "Commandes",
                newName: "IX_Commandes_tableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Tables_tableId",
                table: "Commandes",
                column: "tableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
