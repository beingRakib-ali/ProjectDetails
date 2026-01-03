using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectDetails.Migrations
{
    /// <inheritdoc />
    public partial class addfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Product_Tbl",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "Product_Tbl",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialCode",
                table: "Product_Tbl",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "Product_Tbl",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Review",
                table: "Product_Tbl",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKU",
                table: "Product_Tbl",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Product_Tbl",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Product_Tbl");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Product_Tbl");

            migrationBuilder.DropColumn(
                name: "MaterialCode",
                table: "Product_Tbl");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "Product_Tbl");

            migrationBuilder.DropColumn(
                name: "Review",
                table: "Product_Tbl");

            migrationBuilder.DropColumn(
                name: "SKU",
                table: "Product_Tbl");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Product_Tbl");
        }
    }
}
