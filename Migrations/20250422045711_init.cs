using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectDetails.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "paymentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ProjectName = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    TotalCost = table.Column<string>(nullable: false),
                    ShahadotAmount = table.Column<string>(nullable: false),
                    JonyAmount = table.Column<string>(nullable: false),
                    ShahadotStatus = table.Column<string>(nullable: false),
                    JonyStatus = table.Column<string>(nullable: false),
                    PaidAmountPercentage = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "paymentDetails");
        }
    }
}
