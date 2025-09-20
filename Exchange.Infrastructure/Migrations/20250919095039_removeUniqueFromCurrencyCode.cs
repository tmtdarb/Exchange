using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeUniqueFromCurrencyCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Currencies_CurrencyCode",
                table: "Currencies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Currencies_CurrencyCode",
                table: "Currencies",
                column: "CurrencyCode",
                unique: true);
        }
    }
}
