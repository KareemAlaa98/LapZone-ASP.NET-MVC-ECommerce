using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_GP.Data.Migrations
{
    /// <inheritdoc />
    public partial class modify_building_and_floor_Numbers_Check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_BuildingNumber_IsPositive",
                table: "AspNetUsers");

            migrationBuilder.DropCheckConstraint(
                name: "CK_FloorNumber_IsPositive",
                table: "AspNetUsers");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BuildingNumber_IsPositive",
                table: "AspNetUsers",
                sql: "[Building_Number] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_FloorNumber_IsPositive",
                table: "AspNetUsers",
                sql: "[Floor_Number] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_BuildingNumber_IsPositive",
                table: "AspNetUsers");

            migrationBuilder.DropCheckConstraint(
                name: "CK_FloorNumber_IsPositive",
                table: "AspNetUsers");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BuildingNumber_IsPositive",
                table: "AspNetUsers",
                sql: "[Building_Number] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_FloorNumber_IsPositive",
                table: "AspNetUsers",
                sql: "[Floor_Number] > 0");
        }
    }
}
