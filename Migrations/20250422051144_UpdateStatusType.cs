using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectDetails.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStatusType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "paymentDetails",
                nullable: false,
                oldClrType: typeof(string));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "paymentDetails",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
