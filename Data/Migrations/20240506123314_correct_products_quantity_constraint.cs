using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_GP.Data.Migrations
{
    /// <inheritdoc />
    public partial class correct_products_quantity_constraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_stock_is_positive",
                table: "Products");

            migrationBuilder.AddCheckConstraint(
                name: "CK_stock_is_positive",
                table: "Products",
                sql: "[QuantityInStock] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_stock_is_positive",
                table: "Products");

            migrationBuilder.AddCheckConstraint(
                name: "CK_stock_is_positive",
                table: "Products",
                sql: "[QuantityInStock] > 0");
        }
    }
}
