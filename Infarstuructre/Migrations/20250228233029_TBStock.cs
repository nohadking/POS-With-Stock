using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class TBStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBStocks",
                columns: table => new
                {
                    IdStock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWarehouse = table.Column<int>(type: "int", nullable: false),
                    BondType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BondNumber = table.Column<int>(type: "int", nullable: false),
                    BondDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    InputQuantity = table.Column<int>(type: "int", nullable: false),
                    OutputQuantity = table.Column<int>(type: "int", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBStocks", x => x.IdStock);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBStocks");
        }
    }
}
