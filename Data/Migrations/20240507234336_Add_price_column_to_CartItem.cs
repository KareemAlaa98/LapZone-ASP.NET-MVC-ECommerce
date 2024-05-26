using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_GP.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_price_column_to_CartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartItems");
        }
    }
}
