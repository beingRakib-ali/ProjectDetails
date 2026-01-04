using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectDetails.Migrations
{
    /// <inheritdoc />
    public partial class InitialHostingiiii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Blogs_Tbl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Blogs_Tbl",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
