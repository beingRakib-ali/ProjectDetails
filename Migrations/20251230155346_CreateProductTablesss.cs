using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectDetails.Migrations
{
    /// <inheritdoc />
    public partial class CreateProductTablesss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Tbl_Product_Tbl_ProductId",
                table: "ProductImage_Tbl");

            migrationBuilder.DropIndex(
                name: "IX_ProductImage_Tbl_ProductId",
                table: "ProductImage_Tbl");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ProductImage_Tbl",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "ProductImage_Tbl",
                newName: "ProductImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "ProductImage_Tbl",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ProductImageId",
                table: "ProductImage_Tbl",
                newName: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_Tbl_ProductId",
                table: "ProductImage_Tbl",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Tbl_Product_Tbl_ProductId",
                table: "ProductImage_Tbl",
                column: "ProductId",
                principalTable: "Product_Tbl",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
