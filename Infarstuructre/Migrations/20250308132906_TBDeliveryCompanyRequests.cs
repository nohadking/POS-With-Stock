using System;
using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Infarstuructre.Migrations
{
    /// <inheritdoc />
    public partial class TBDeliveryCompanyRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBDeliveryCompanyRequestss",
                columns: table => new
                {
                    IdDeliveryCompanyRequests = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDeliveryCompanie = table.Column<int>(type: "int", nullable: false),
                    NameClint = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IdDeliveryCompanyPricing = table.Column<int>(type: "int", nullable: false),
                    FullAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Nouts = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    DateTimeInvose = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdPaymentMethod = table.Column<int>(type: "int", nullable: false),
                    IdTypeOrder = table.Column<int>(type: "int", nullable: false),
                    DataEntry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeEntry = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBDeliveryCompanyRequestss", x => x.IdDeliveryCompanyRequests);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBDeliveryCompanyRequestss");
        }
    }
}
