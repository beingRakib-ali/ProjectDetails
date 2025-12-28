using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectDetails.Migrations
{
    /// <inheritdoc />
    public partial class addCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category_Tbl",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    keyEntry1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    keyEntry2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    keyEntry3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    keyEntry4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    keyEntry5 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_Tbl", x => x.CategoryId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category_Tbl");
        }
    }
}
