using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CurrencyNameEn = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Conversions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecievedCurrencyID = table.Column<int>(type: "int", nullable: false),
                    SoldCurrencyID = table.Column<int>(type: "int", nullable: false),
                    AmountToBuy = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    AmountToSell = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    ConversionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Conversions_Currencies_RecievedCurrencyID",
                        column: x => x.RecievedCurrencyID,
                        principalTable: "Currencies",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Conversions_Currencies_SoldCurrencyID",
                        column: x => x.SoldCurrencyID,
                        principalTable: "Currencies",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyID = table.Column<int>(type: "int", nullable: false),
                    BuyRate = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    SellRate = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_Currencies_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "Currencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_RecievedCurrencyID",
                table: "Conversions",
                column: "RecievedCurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_SoldCurrencyID",
                table: "Conversions",
                column: "SoldCurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_CurrencyCode",
                table: "Currencies",
                column: "CurrencyCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_OrderNumber",
                table: "Currencies",
                column: "OrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CurrencyID",
                table: "ExchangeRates",
                column: "CurrencyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conversions");

            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
