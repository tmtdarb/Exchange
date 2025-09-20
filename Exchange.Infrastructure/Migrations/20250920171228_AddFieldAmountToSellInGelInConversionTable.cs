using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldAmountToSellInGelInConversionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountToSellInGel",
                table: "Conversions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountToSellInGel",
                table: "Conversions");
        }
    }
}
