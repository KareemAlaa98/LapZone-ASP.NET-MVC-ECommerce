using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_GP.Data.Migrations
{
    /// <inheritdoc />
    public partial class correct_products_table_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Prodcuts_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Prodcuts_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdcutImages_Prodcuts_ProductId",
                table: "ProdcutImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Prodcuts_Brands_BrandId",
                table: "Prodcuts");

            migrationBuilder.DropForeignKey(
                name: "FK_Prodcuts_Categories_CategoryId",
                table: "Prodcuts");

            migrationBuilder.DropForeignKey(
                name: "FK_Prodcuts_Discounts_DiscountId",
                table: "Prodcuts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWishlist_Prodcuts_ProductsId",
                table: "ProductWishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Prodcuts_ProductId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prodcuts",
                table: "Prodcuts");

            migrationBuilder.RenameTable(
                name: "Prodcuts",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_Prodcuts_DiscountId",
                table: "Products",
                newName: "IX_Products_DiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_Prodcuts_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Prodcuts_BrandId",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdcutImages_Products_ProductId",
                table: "ProdcutImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWishlist_Products_ProductsId",
                table: "ProductWishlist",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdcutImages_Products_ProductId",
                table: "ProdcutImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWishlist_Products_ProductsId",
                table: "ProductWishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Prodcuts");

            migrationBuilder.RenameIndex(
                name: "IX_Products_DiscountId",
                table: "Prodcuts",
                newName: "IX_Prodcuts_DiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Prodcuts",
                newName: "IX_Prodcuts_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Prodcuts",
                newName: "IX_Prodcuts_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prodcuts",
                table: "Prodcuts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Prodcuts_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Prodcuts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Prodcuts_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Prodcuts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdcutImages_Prodcuts_ProductId",
                table: "ProdcutImages",
                column: "ProductId",
                principalTable: "Prodcuts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prodcuts_Brands_BrandId",
                table: "Prodcuts",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prodcuts_Categories_CategoryId",
                table: "Prodcuts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodcuts_Discounts_DiscountId",
                table: "Prodcuts",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWishlist_Prodcuts_ProductsId",
                table: "ProductWishlist",
                column: "ProductsId",
                principalTable: "Prodcuts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Prodcuts_ProductId",
                table: "Reviews",
                column: "ProductId",
                principalTable: "Prodcuts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
