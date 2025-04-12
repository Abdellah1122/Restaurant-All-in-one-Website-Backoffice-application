using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class ML1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiquetsArchives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tableId = table.Column<int>(type: "int", nullable: false),
                    DateTiquet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModePayment = table.Column<int>(type: "int", nullable: false),
                    TotalTiquet = table.Column<double>(type: "float", nullable: false),
                    MontantDonne = table.Column<double>(type: "float", nullable: false),
                    Reste = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiquetsArchives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiquetsArchives_Tables_tableId",
                        column: x => x.tableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TiquetsArchives_tableId",
                table: "TiquetsArchives",
                column: "tableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiquetsArchives");
        }
    }
}
